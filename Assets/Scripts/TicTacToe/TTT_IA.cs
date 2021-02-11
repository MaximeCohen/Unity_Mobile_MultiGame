using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace TicTacToe
{
    public class TTT_IA : MonoBehaviour
    {
        public enum IAState
        {
            DUMB = 0,
            EASY,
            NORMAL,
            HARD
        }

        public UnityAction<int> OnComputeDone = null;

        [SerializeField] private IAState _state = IAState.EASY;

        private int _moveFound;


        private int FoundRandomEmptyCell(TTT_Cell[] cells)
        {
            var noneCells = Array.FindAll(cells, cell => cell.CurrentShape == Shape.NONE);
            var random = new System.Random();
            int noneIndex = random.Next(0, noneCells.Length);
            return Array.IndexOf(cells, noneCells[noneIndex]);
        }

        private (int, int) CountShape(TTT_Cell[] cells, Shape iaShape, (int, int, int) line)
        {
            var iaShapeCount = 0;
            var enemyShapeCount = 0;
            foreach (var index in new int[]{ line.Item1, line.Item2, line.Item3 })
            {
                if (cells[index].CurrentShape == iaShape)
                    ++iaShapeCount;
                else if (cells[index].CurrentShape != Shape.NONE)
                    ++enemyShapeCount;
            }
            return (iaShapeCount, enemyShapeCount);
        }

        private int FoundCellScore(TTT_Cell[] cells, Shape iaShape, int indexCell, Dictionary<(int, int), int> tableScore)
        {
            var lines = new List<(int, int, int)> { (0, 1, 2), (3, 4, 5), (6, 7, 8), (0, 3, 6), (1, 4, 7), (2, 5, 8), (0, 4, 8), (2, 4, 6) };
            var cellLines = lines.FindAll(val => val.Item1 == indexCell || val.Item2 == indexCell || val.Item3 == indexCell);
            var score = 0;
            foreach (var cellLine in cellLines)
            {
                score += tableScore[CountShape(cells, iaShape, cellLine)];
            }
            return score;
        }

        private int FoundBestCellByScore(TTT_Cell[] cells, Shape iaShape, Dictionary<(int, int), int> tableScore)
        {
            var scoreCells = new int[cells.Length];
            var bestScore = 0;
            for (int i = 0; i < cells.Length; i++)
            {
                scoreCells[i] = cells[i].CurrentShape == Shape.NONE ? FoundCellScore(cells, iaShape, i, tableScore) : 0;
                if (scoreCells[i] > bestScore)
                    bestScore = scoreCells[i];
            }

            Debug.Log($"\n{scoreCells[0]},{scoreCells[1]},{scoreCells[2]}\n" +
                $"{scoreCells[3]},{scoreCells[4]},{scoreCells[5]}\n" +
                $"{scoreCells[6]},{scoreCells[7]},{scoreCells[8]}\n");

            var bestCells = scoreCells.Select((s, i) => s == bestScore ? i : -1).Where(i => i >= 0).ToArray();
            var random = new System.Random();
            int bestIndex = random.Next(0, bestCells.Length);
            return bestCells[bestIndex];
        }

        private void ComputeDumb(TTT_Cell[] cells, Shape iaShape)
        {
            _moveFound = FoundRandomEmptyCell(cells);
        }

        private void ComputeEasy(TTT_Cell[] cells, Shape iaShape)
        {
            var tableScore = new Dictionary<(int, int), int>();
            tableScore[(0, 0)] = 1;
            tableScore[(0, 1)] = 0;
            tableScore[(1, 0)] = 2;
            tableScore[(1, 1)] = 0;
            tableScore[(0, 2)] = 0;
            tableScore[(2, 0)] = 9;

            _moveFound = FoundBestCellByScore(cells, iaShape, tableScore);
        }

        private void ComputeNormal(TTT_Cell[] cells, Shape iaShape)
        {
            var tableScore = new Dictionary<(int, int), int>();
            tableScore[(0, 0)] = 1;
            tableScore[(0, 1)] = 2;
            tableScore[(1, 0)] = 2;
            tableScore[(1, 1)] = 1;
            tableScore[(0, 2)] = 5;
            tableScore[(2, 0)] = 9;

            _moveFound = FoundBestCellByScore(cells, iaShape, tableScore);
        }

        public void ChangeDifficult(IAState state)
        {
            _state = state;
        }

        public void Compute(TTT_Cell[] cells, Shape iaShape)
        {
            switch (_state)
            {
                case IAState.DUMB:
                    ComputeDumb(cells, iaShape);
                    break;
                case IAState.EASY:
                    ComputeEasy(cells, iaShape);
                    break;
                case IAState.NORMAL:
                    ComputeNormal(cells, iaShape);
                    break;
                case IAState.HARD:
                    break;
            }
            StartCoroutine(WaitBeforeComputeDoneCoroutine(1));
        }

        IEnumerator WaitBeforeComputeDoneCoroutine(int s)
        {
            yield return new WaitForSeconds(s);
            OnComputeDone?.Invoke(_moveFound);
        }
    }
}
