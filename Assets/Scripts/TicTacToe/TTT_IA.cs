using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TicTacToe
{
    public class TTT_IA : MonoBehaviour
    {
        public enum Difficult
        {
            EASY,
            NORMAL,
            HARD
        }

        public UnityAction<int> OnComputeDone = null;

        [SerializeField] public Difficult _difficult;

        private int _moveFound;

        private void ComputeEasy(TTT_Cell[] cells)
        {
            var noneCells = Array.FindAll(cells, cell => cell.CurrentState == State.NONE);
            var random = new System.Random();
            int noneIndex = random.Next(0, noneCells.Length);
            _moveFound = Array.IndexOf(cells, noneCells[noneIndex]);
        }

        public void Compute(TTT_Cell[] cells)
        {
            ComputeEasy(cells);
            StartCoroutine(WaitBeforeComputeDoneCoroutine(1));
        }

        IEnumerator WaitBeforeComputeDoneCoroutine(int s)
        {
            yield return new WaitForSeconds(s);
            OnComputeDone?.Invoke(_moveFound);
        }
    }
}
