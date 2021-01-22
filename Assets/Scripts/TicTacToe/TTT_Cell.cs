using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TicTacToe
{
    [Serializable]
    public enum Shape
    {
        NONE = 0,
        CROSS = 1,
        CIRCLE = 2
    }

    public class TTT_Cell : MonoBehaviour, IPointerClickHandler
    {
        public UnityAction OnClick = null;

        [Header("References")]
        [SerializeField] private Image _imageShape = null;
        [SerializeField] private Sprite _spriteCross = null;
        [SerializeField] private Sprite _spriteCircle = null;

        private Shape _currentShape = Shape.NONE;

        public Shape CurrentShape
        {
            get => _currentShape;
            set
            {
                SetImageShapeByShape(value);
                _currentShape = value;
            }
        }


        private void SetImageShapeByShape(Shape state)
        {
            switch (state)
            {
                case Shape.NONE:
                    _imageShape.sprite = null;
                    _imageShape.color = Color.clear;
                    break;
                case Shape.CROSS:
                    _imageShape.sprite = _spriteCross;
                    _imageShape.color = Color.white;
                    break;
                case Shape.CIRCLE:
                    _imageShape.sprite = _spriteCircle;
                    _imageShape.color = Color.white;
                    break;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            
            OnClick?.Invoke();
            Debug.Log($"Cell touched");
        }
    }

}
