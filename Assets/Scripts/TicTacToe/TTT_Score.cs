using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe
{
    public class TTT_Score : MonoBehaviour
    {
        [Serializable]
        private struct Score
        {
            public Image Shape;
            public TextMeshProUGUI Text;
            [NonSerialized] public int value;
        }

        [Header("References")]
        [SerializeField] private TTT_Board _board = null;

        [SerializeField] private Score _scorePlayer;
        [SerializeField] private Score _scoreIA;

        private void Start()
        {
            _board.OnGameDone += HandleGameDone;
            _scorePlayer.value = 0;
            _scoreIA.value = 0;
            RefreshText();
        }

        private void RefreshText()
        {
            _scorePlayer.Text.text = $"{_scorePlayer.value}";
            _scoreIA.Text.text = $"{_scoreIA.value}";
        }

        private void HandleGameDone(Result result)
        {
            switch (result)
            {
                case Result.WIN:
                    ++_scorePlayer.value;
                    break;
                case Result.LOOSE:
                    ++_scoreIA.value;
                    break;
            }
            RefreshText();
        }
    }
}