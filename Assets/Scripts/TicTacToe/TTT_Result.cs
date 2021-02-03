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


        private void Awake()
        {
            _board.OnGameStart += HandleGameStart;
            _board.OnGameDone += HandleGameDone;
        }

        private void HandleGameStart()
        {
            _text.text = "";
        }

        private void WriteText(Color color, string text)
        {
            _text.color = color;
            _text.text = text;
        }

        private void HandleGameDone(Result result)
        {
            switch (result)
            {
                case Result.PLAYER_ONE_WIN:
                    WriteText(_win, "You Win !");
                    break;
                case Result.PLAYER_TWO_WIN:
                    WriteText(_loose, "You Loose !");
                    break;
                case Result.DRAW:
                    WriteText(_draw, "Draw !");
                    break;
            }
        }

    }
}