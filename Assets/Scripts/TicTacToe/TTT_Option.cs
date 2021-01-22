using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe
{
    public class TTT_Option : MonoBehaviour
    {
        [SerializeField] private GameObject _mainOption = null;
        [SerializeField] private GameObject _shapeOption = null;

        public void Show()
        {
            gameObject.SetActive(true);
            _mainOption.SetActive(true);
            _shapeOption.SetActive(false);
        }

        public void ShowShape()
        {
            _mainOption.SetActive(false);
            _shapeOption.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}