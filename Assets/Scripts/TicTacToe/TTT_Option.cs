using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static TicTacToe.TTT_IA;

namespace TicTacToe
{
    [Serializable]
    public class SelectShapeEvent : UnityEvent<Shape, Shape> { };

    [Serializable]
    public class SelectDifficultEvent : UnityEvent<IAState> { };

    public class TTT_Option : MonoBehaviour
    {
        [SerializeField] private TTT_ShapeScriptableObject _data = null;
        [SerializeField] private GameObject _shapeButtonPrefab = null;

        [Header("References")]
        [SerializeField] private GameObject _mainOption = null;
        [SerializeField] private GameObject _shapeOption = null;
        [SerializeField] private GameObject _difficultyOption = null;
        [SerializeField] private GameObject _shapeButtons = null;
        [SerializeField] private TextMeshProUGUI _title;

        [Header("Events")]
        [SerializeField] public SelectShapeEvent OnSelectShape = null;
        [SerializeField] public UnityEvent OnClickReset = null;
        [SerializeField] public SelectDifficultEvent OnSelectDifficult = null;

        private Shape _playerOneShapeSelected = Shape.NONE;

        private List<Button> _buttons = new List<Button>();


        private void Start()
        {
            _title.text = $"Select Player 1 Shape";
            foreach (var shapeData in _data.ShapeDatas)
            {
                var obj = Instantiate(_shapeButtonPrefab, _shapeButtons.transform);
                obj.name += $"({shapeData.shape})";
                var image = Array.Find(obj.GetComponentsInChildren<Image>(), img => img.gameObject.GetInstanceID() != obj.GetInstanceID());
                image.sprite = shapeData.sprite;
                var button = obj.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    button.interactable = false;
                    if (_playerOneShapeSelected == Shape.NONE)
                        SelectPlayerOneShape(shapeData.shape);
                    else
                        SelectPlayerTwoShape(shapeData.shape);
                });
                _buttons.Add(button);
            }
        }

        private void SelectPlayerOneShape(Shape shape)
        {
            _playerOneShapeSelected = shape;
            _title.text = $"Select Player 2 Shape";
        }

        private void SelectPlayerTwoShape(Shape shape)
        {
            ResetButton();
            ShowMain();
            _title.text = $"Select Player 1 Shape";
            OnSelectShape?.Invoke(_playerOneShapeSelected, shape);
            _playerOneShapeSelected = Shape.NONE;
        }

        private void ResetButton()
        {
            _buttons.ForEach(button => button.interactable = true);
        }

        public void ResetScore()
        {
            gameObject.SetActive(false);
            OnClickReset?.Invoke();
        }

        public void ShowMain()
        {
            gameObject.SetActive(true);
            _mainOption.SetActive(true);
            _shapeOption.SetActive(false);
            _difficultyOption.SetActive(false);
        }

        public void ShowShape()
        {
            gameObject.SetActive(true);
            _mainOption.SetActive(false);
            _shapeOption.SetActive(true);
            _difficultyOption.SetActive(false);
        }

        public void ShowDifficulty()
        {
            gameObject.SetActive(true);
            _mainOption.SetActive(false);
            _shapeOption.SetActive(false);
            _difficultyOption.SetActive(true);
        }

        public void SelectDifficulty(int level)
        {
            ShowMain();
            OnSelectDifficult?.Invoke((IAState)level);
        }

        public void Hide()
        {
            _playerOneShapeSelected = Shape.NONE;
            gameObject.SetActive(false);
        }
    }
}