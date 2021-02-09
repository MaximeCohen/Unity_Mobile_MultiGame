using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TicTacToe
{
    public class TTT_Result : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TTT_Board _board = null;
        [SerializeField] private TextMeshProUGUI _text;

        [Header("Colors")]
        [SerializeField] private Color _normal = Color.white;
        [SerializeField] private Color _win = Color.green;
        [SerializeField] private Color _loose = Color.red;
        [SerializeField] private Color _draw = Color.yellow;

        private bool _isMultiplayer = false;


        private void Awake()
        {
            _board.OnGameStart += HandleGameStart;
            _board.OnTurnDone += HandleTurnDone;
            _board.OnGameDone += HandleGameDone;
        }

        private void HandleTurnDone(bool isPlayerOneTurn)
        {
            if (!_isMultiplayer) return;
            if (isPlayerOneTurn)
                WriteText(_normal, "Player 1 turn");
            else
                WriteText(_normal, "Player 2 turn");
        }

        private void HandleGameStart(bool isMultiplayer)
        {
            _isMultiplayer = isMultiplayer;
            WriteText(_normal, _isMultiplayer ? "Player 1 turn" : "");
        }

        private void HandleGameDone(Result result)
        {
            switch (result)
            {
                case Result.PLAYER_ONE_WIN:
                    if (_isMultiplayer)
                        WriteText(_win, "Player 1 Win !");
                    else
                        WriteText(_win, "You Win !");
                    break;
                case Result.PLAYER_TWO_WIN:
                    if (_isMultiplayer)
                        WriteText(_win, "Player 2 Win !");
                    else
                        WriteText(_loose, "You Loose !");
                    break;
                case Result.DRAW:
                    WriteText(_draw, "Draw !");
                    break;
            }
        }

        private void WriteText(Color color, string text)
        {
            _text.color = color;
            _text.text = text;
        }
    }
}