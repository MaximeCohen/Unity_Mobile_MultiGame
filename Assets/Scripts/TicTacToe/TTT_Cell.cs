using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TicTacToe
{

    public class TTT_Cell : MonoBehaviour, IPointerClickHandler
    {
        public UnityAction OnClick = null;

        [Header("References")]
        [SerializeField] private TTT_ShapeScriptableObject _data = null;
        [SerializeField] private Image _imageShape = null;

        private Shape _currentShape = Shape.NONE;

        public Shape CurrentShape
        {
            get => _currentShape;
            set
            {
                SetImageByShape(value);
                _currentShape = value;
            }
        }


        private void SetImageByShape(Shape shape)
        {
            _imageShape.sprite = _data.GetSpriteByShape(shape);
            _imageShape.color = shape == Shape.NONE ? Color.clear : Color.white;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            
            OnClick?.Invoke();
            Debug.Log($"Cell touched");
        }
    }

}
