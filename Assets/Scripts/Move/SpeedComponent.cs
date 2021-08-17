﻿using UnityEngine;
using UseEvents;

namespace UseMove
{
    public class SpeedComponent : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _speedPerClick;
        [SerializeField] private float _speedReduction;
        [SerializeField] private float _baseSpeedReduction;

        private bool _isStop;

        public bool IsStop
        {
            get { return _isStop; }
            private set { _isStop = value; }
        }
        public float Speed
        {
            get
            {
                return _speed;
            }
            private set
            {
                _speed = value;
            }
        }
        public float MaxSpeed => _maxSpeed;

        private void OnEnable()
        {
            SpeedChangedEvent.OnAction += Validate;
            OnPlayerClickedEvent.OnAction += AddSpeedPerClick;
        }
        private void OnDisable()
        {
            SpeedChangedEvent.OnAction -= Validate;
            OnPlayerClickedEvent.OnAction -= AddSpeedPerClick;
        }

        private void FixedUpdate()
        {
            ChangeSpeedReduction();
            ReduceSpeed();
            Validate();
        }

        private void AddSpeedPerClick()
        {
            Speed += _speedPerClick;
            SpeedChangedEvent.ActivateEvent();
        }
        public void Stop()
        {
            Speed = 0;
            IsStop = true;
            SpeedChangedEvent.ActivateEvent();
        }
        public void UnStop() => IsStop = false;

        private void ReduceSpeed()
        {
            if (Speed > 0)
            {
                Speed -= _speedReduction;
                SpeedChangedEvent.ActivateEvent();
            }
        }
        private void ChangeSpeedReduction()
        {
            _speedReduction = (Speed / _maxSpeed) * 0.2f + _baseSpeedReduction;
        }
        private void Validate()
        {
            if (Speed < 0)
                Stop();
        }
    }
}