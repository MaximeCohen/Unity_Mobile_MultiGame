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
        [SerializeField] private Color _win = Color.green;
        [SerializeField] private Color _loose = Color.red;
        [SerializeField] private Color _draw = Color.yellow;

        private void Start()
        {
            _board.OnGameStart += HandleGameStart;
            _board.OnGameDone += HandleGameDone;
        }

        private void HandleGameStart()
        {
            _text.text = "";
        }

        private void HandleGameDone(Result result)
        {
            switch (result)
            {
                case Result.WIN:
                    _text.color = _win;
                    _text.text = "You Win !";
                    break;
                case Result.LOOSE:
                    _text.color = _loose;
                    _text.text = "You Loose !";
                    break;
                case Result.DRAW:
                    _text.color = _draw;
                    _text.text = "Draw !";
                    break;
            }
        }

    }
}