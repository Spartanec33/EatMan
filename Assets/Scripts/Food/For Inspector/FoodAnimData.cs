using System;
using System.Collections;
using UnityEngine;

namespace UseFoodComponent
{
    [Serializable]
    public struct FoodAnimData
    {
        [SerializeField] private float _verticalSpeed;
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private float _verticalRange;

        public float VerticalSpeed => _verticalSpeed;
        public float HorizontalSpeed => _horizontalSpeed;
        public float VerticalRange => _verticalRange;
    }
}