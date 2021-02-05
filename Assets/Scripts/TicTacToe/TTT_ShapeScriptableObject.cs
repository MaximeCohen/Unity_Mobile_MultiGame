using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe
{
    [Serializable]
    public enum Shape
    {
        NONE = 0,
        CROSS = 1,
        CIRCLE = 2,
        SQUARE
    }

    [CreateAssetMenu(fileName = "Shapes", menuName = "Datas/TicTacToe/Shapes", order = 1)]
    public class TTT_ShapeScriptableObject : ScriptableObject
    {
        [Serializable]
        public struct ShapeData
        {
            public Shape shape;
            public Sprite sprite;
        }

        [SerializeField] public Shape DefaultPlayerOneShape = Shape.CROSS;
        [SerializeField] public Shape DefaultPlayerTwoShape = Shape.CIRCLE;

        [SerializeField] public ShapeData[] ShapeDatas;


        public Sprite GetSpriteByShape(Shape shape)
        {
            return Array.Find(ShapeDatas, data => data.shape == shape).sprite;
        }
    }
}
