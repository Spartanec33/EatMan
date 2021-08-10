using UnityEngine;

public class SpeedComponent: MonoBehaviour
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
    private void FixedUpdate()
    {
        ChangeSpeedReduction();
        ReduceSpeed();
        Validate();
    }

    public void AddSpeedPerClick() => Speed += _speedPerClick;
    public void Stop()
    {
        Speed = 0;
        IsStop = true;
    }
    public void UnStop() => IsStop = false;

    private void ReduceSpeed()
    {
        if (Speed>0)
            Speed -= _speedReduction;
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