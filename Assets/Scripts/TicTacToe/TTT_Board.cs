using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TicTacToe
{
    [Serializable]
    public struct Cells
    {
        public TTT_Cell UpperLeft;
        public TTT_Cell UpperCenter;
        public TTT_Cell UpperRight;
        public TTT_Cell MiddleLeft;
        public TTT_Cell MiddleCenter;
        public TTT_Cell MiddleRight;
        public TTT_Cell LowerLeft;
        public TTT_Cell LowerCenter;
        public TTT_Cell LowerRight;

        public TTT_Cell[] GetArray() => new TTT_Cell[] {
            UpperLeft, UpperCenter, UpperRight,
            MiddleLeft, MiddleCenter, MiddleRight,
            LowerLeft, LowerCenter, LowerRight
        };
    }

    public enum Result
    {
        WIN,
        LOOSE,
        DRAW
    }

    public class TTT_Board : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CanvasGroup _cellsCanvasGroup = null;
        [SerializeField] private TTT_IA _iA = null;
        [SerializeField] private Cells _arrayCell;

        public UnityAction<Result> OnGameDone = null;
        public UnityAction<bool> OnTurnDone = null;
        public UnityAction OnGameStart = null;

        private Shape _playerOneShape = Shape.CROSS;

        private TTT_Cell[] _cells;

        private (int, int, int) _lineDone;

        private bool _playerOneTurn = true;


        private void Awake()
        {
            _iA.OnComputeDone += HandleIaSelectCell;
        }

        private void Start()
        {
            _cells = _arrayCell.GetArray();
            for (int i = 0; i < _cells.Length; i++)
            {
                int index = i;
                _cells[i].OnClick += () => HandlePlayerClickCell(index);
            }
            OnGameStart?.Invoke();
        }

        private bool IsLineSameShape(int index1, int index2, int index3)
        {
            if (_cells[index1].CurrentShape != Shape.NONE
                && _cells[index1].CurrentShape == _cells[index2].CurrentShape
                && _cells[index1].CurrentShape == _cells[index3].CurrentShape)
                return true;
            return false;

        }

        private bool CheckBoardFull()
        {
            return Array.TrueForAll(_cells, cell => cell.CurrentShape != Shape.NONE);
        }

        private bool CheckGameDone()
        {
            var lines = new List<(int, int, int)> { (0, 1, 2), (3, 4, 5), (6, 7, 8), (0, 3, 6), (1, 4, 7), (2, 5, 8), (0, 4, 8), (2, 4, 6) };
            foreach (var line in lines)
            {
                if (IsLineSameShape(line.Item1, line.Item2, line.Item3))
                {
                    _lineDone = line;
                    return true;
                }
            }
            return false;
        }

        private void TurnDone()
        {
            if (CheckGameDone())
            {
                var playerResult = _cells[_lineDone.Item1].CurrentShape == _playerOneShape ? Result.WIN : Result.LOOSE;
                _cellsCanvasGroup.blocksRaycasts = false;
                Debug.Log($"You {playerResult} !");
                OnGameDone?.Invoke(playerResult);
            }
            else if (CheckBoardFull())
            {
                _cellsCanvasGroup.blocksRaycasts = false;
                Debug.Log("Draw !");
                OnGameDone?.Invoke(Result.DRAW);
            }
            else
            {
                _playerOneTurn = !_playerOneTurn;
                _cellsCanvasGroup.blocksRaycasts = _playerOneTurn;
                if (!_playerOneTurn)
                    _iA.Compute(_cells);
                OnTurnDone?.Invoke(_playerOneTurn);
            }
        }

        public void RestartGame()
        {
            foreach (var cell in _cells)
            {
                cell.CurrentShape = Shape.NONE;
            }
            _playerOneTurn = true;
            _cellsCanvasGroup.blocksRaycasts = true;
            OnGameStart?.Invoke();
        }

        public void ChangeShape(int shapeIndex)
        {
            _playerOneShape = (Shape)shapeIndex;
            RestartGame();
        }

        #region Event Handler

        private void HandlePlayerClickCell(int index)
        {
            if (_cells[index].CurrentShape != Shape.NONE)
                return;
            _cells[index].CurrentShape = _playerOneShape;
            TurnDone();
        }

        private void HandleIaSelectCell(int index)
        {
            _cells[index].CurrentShape = _playerOneShape == Shape.CROSS ? Shape.CIRCLE : Shape.CROSS;
            TurnDone();
        }

        #endregion
    }
}