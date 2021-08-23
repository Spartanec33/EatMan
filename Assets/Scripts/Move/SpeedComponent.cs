using UnityEngine;
using UseEvents;

namespace UseMove
{
    public class SpeedComponent : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _speedLimit;
        [SerializeField] private float _speedPerClick;
        [SerializeField] private float _speedReduction;
        [SerializeField] private float _baseSpeedReduction;

        private bool _isStop;
        private int _maxSpeed;

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
                OnSpeedChanged.ActivateEvent();
                Validate();
                if (_speed > _maxSpeed)
                    _maxSpeed = (int)_speed;
            }
        }
        public float SpeedLimit => _speedLimit;
        public int MaxSpeed => _maxSpeed;

        private void OnEnable()
        {
            OnPlayerClick.OnAction += AddSpeedPerClick;
        }
        private void OnDisable()
        {
            OnPlayerClick.OnAction -= AddSpeedPerClick;
        }

        private void FixedUpdate()
        {
            ChangeSpeedReduction();
            ReduceSpeed();
        }

        private void AddSpeedPerClick()
        {
            Speed += _speedPerClick;
        }
        public void Stop()
        {
            Speed = 0;
            IsStop = true;
        }
        public void UnStop() => IsStop = false;

        private void ReduceSpeed()
        {
            if (Speed > 0)
            {
                Speed -= _speedReduction;
            }
        }
        private void ChangeSpeedReduction()
        {
            _speedReduction = (Speed / _speedLimit) * 0.2f + _baseSpeedReduction;
        }
        private void Validate()
        {
            if (Speed < 0)
                Stop();
        }
    }
}