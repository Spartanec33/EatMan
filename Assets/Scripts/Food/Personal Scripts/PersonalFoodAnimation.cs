using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UseFoodComponent
{
    public class PersonalFoodAnimation : MonoBehaviour
    {
        private bool _upward = true;
        private int _quantity;
        private int _currentQuantity;
        private FoodAnimData _foodAnimData;

        private void Start()
        {
            _quantity = (int)(_foodAnimData.VerticalRange / _foodAnimData.VerticalSpeed);
        }

        private void FixedUpdate()
        {
            Rotate();
            Move();
        }

        private void Rotate()
        {
            transform.Rotate(0, _foodAnimData.HorizontalSpeed, 0);
        }
        private void Move()
        {
            int direction = _upward ? 1 : -1;
            transform.position += new Vector3(0, direction * _foodAnimData.VerticalSpeed, 0);
            _currentQuantity++;
            if (_currentQuantity == _quantity)
            {
                _currentQuantity = 0;
                _upward = !_upward;
            }
        }
        public void Init(FoodAnimData data)
        {
            _foodAnimData = data;
        }
    }
}