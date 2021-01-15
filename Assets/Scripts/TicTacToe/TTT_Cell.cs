using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TicTacToe
{
    public enum State
    {
        NONE,
        CROSS,
        CIRCLE
    }

    public class TTT_Cell : MonoBehaviour, IPointerClickHandler
    {
        public UnityAction OnClick = null;

        [Header("References")]
        [SerializeField] private Image _imageShape = null;
        [SerializeField] private Sprite _spriteCross = null;
        [SerializeField] private Sprite _spriteCircle = null;

        private State _currentState = State.NONE;

        public State CurrentState
        {
            get => _currentState;
            set
            {
                SetImageShapeByState(value);
                _currentState = value;
            }
        }


        private void SetImageShapeByState(State state)
        {
            switch (state)
            {
                case State.NONE:
                    _imageShape.sprite = null;
                    _imageShape.color = Color.clear;
                    break;
                case State.CROSS:
                    _imageShape.sprite = _spriteCross;
                    _imageShape.color = Color.white;
                    break;
                case State.CIRCLE:
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
