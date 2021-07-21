using UnityEngine;
using System;
public class SpeedChanger: MonoBehaviour
{
    [SerializeField] private float _speedPerClick;
    [SerializeField] private float _speedReduction;
    [SerializeField] float _baseSpeedReduction;

    private Mover _mover;

    private float Speed { get => _mover.Speed; set => _mover.Speed = value; }
    private float MaxSpeed { get => _mover.MaxSpeed;}

    private void Start()
    {
        _mover = GetComponent<Mover>();
    }

    private void FixedUpdate()
    {
        ReduceSpeed();
    }

    public void AddSpeedPerClick() => _mover.Speed += _speedPerClick;
    private void ReduceSpeed()
    {
        _speedReduction = (Speed / MaxSpeed) * 0.1f + _baseSpeedReduction;

        Speed = Speed > 0 ? Speed - _speedReduction : 0;
        if (Speed > 0 && Speed < 0.3f)
            Speed = 0;
    }

}