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
            public Image Outline;
            public TextMeshProUGUI Text;
            [NonSerialized] public int value;
        }

        [Header("References")]
        [SerializeField] private TTT_Board _board = null;

        [SerializeField] private Score _scorePlayerOne;
        [SerializeField] private Score _scorePlayerTwo;

        private void Start()
        {
            _board.OnGameDone += HandleGameDone;
            _board.OnTurnDone += HandleTurnDone;
            _board.OnGameStart += HandleGameStart;
            _scorePlayerOne.value = 0;
            _scorePlayerTwo.value = 0;
            RefreshText();
        }

        private void RefreshText()
        {
            _scorePlayerOne.Text.text = $"{_scorePlayerOne.value}";
            _scorePlayerTwo.Text.text = $"{_scorePlayerTwo.value}";
        }

        private void HandleGameStart()
        {
            _scorePlayerOne.Outline.gameObject.SetActive(true);
            _scorePlayerTwo.Outline.gameObject.SetActive(false);
        }

        private void HandleTurnDone(bool isPlayerOne)
        {
            _scorePlayerOne.Outline.gameObject.SetActive(isPlayerOne);
            _scorePlayerTwo.Outline.gameObject.SetActive(!isPlayerOne);
        }

        private void HandleGameDone(Result result)
        {
            _scorePlayerOne.Outline.gameObject.SetActive(false);
            _scorePlayerTwo.Outline.gameObject.SetActive(false);
            switch (result)
            {
                case Result.WIN:
                    ++_scorePlayerOne.value;
                    break;
                case Result.LOOSE:
                    ++_scorePlayerTwo.value;
                    break;
            }
            RefreshText();
        }
    }
}