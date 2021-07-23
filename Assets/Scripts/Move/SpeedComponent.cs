﻿using UnityEngine;
using System;

delegate void MessageHandler();
public class SpeedComponent: MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _speedPerClick;
    [SerializeField] private float _speedReduction;
    [SerializeField] float _baseSpeedReduction;

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
    public void Stop() => Speed = 0;

    private void ReduceSpeed()
    {
        if (Speed>0)
            Speed -= _speedReduction;
    }
    private void ChangeSpeedReduction()
    {
        _speedReduction = (Speed / _maxSpeed) * 0.1f + _baseSpeedReduction;
    }
    private void Validate()
    {
        if (Speed < 0)
            Stop();
    }
}