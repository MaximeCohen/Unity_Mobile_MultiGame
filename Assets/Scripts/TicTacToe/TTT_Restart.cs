using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe
{
    public class TTT_Restart : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TTT_Board _board = null;
        [SerializeField] private Button _button = null;

        private void Awake()
        {
            _button.onClick.AddListener(HandleClickButton);
            _board.OnGameDone += HandleGameDone;
        }

        private void HandleClickButton()
        {
            _board.RestartGame();
            _button.gameObject.SetActive(false);
        }

        private void HandleGameDone(Result result)
        {
            _button.gameObject.SetActive(true);
        }
    }

}