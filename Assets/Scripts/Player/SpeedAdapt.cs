using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UseMove;

namespace UsePlayerComponents
{

    public class SpeedAdapt : MonoBehaviour
    {
        [Header("Animation")]
        [SerializeField] private float _minAnimSpeed;
        [SerializeField] private float _maxAnimSpeed;
        [SerializeField] private float _speedToRun;

        [Header("Particle")]
        [SerializeField] private ParticleSystem _dirtParticle;
        [SerializeField] private float _emissionByMaxSpeed;
        [SerializeField] private float _startSpeedByMaxSpeed;
        [SerializeField] private float _maxStartSizeByMaxSpeed;
        [SerializeField] private float _minStartSizeByMaxSpeed;

        private Animator _animator;
        private SpeedComponent _speedCom;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _speedCom = FindObjectOfType<SpeedComponent>();
        }
        private void Update()
        {
            var Speed = _speedCom.Speed;
            ChangeAnimSpeed(Speed);
            ChangeParticles(Speed);
        }
        private float GetAnimSpeed(float speedNow,float maxSpeed)
        {
            var t = speedNow / maxSpeed;
            var animSpeed = t * _maxAnimSpeed;

            if (animSpeed<_minAnimSpeed)
                animSpeed = _minAnimSpeed;
            
            return animSpeed;
        }
        private void ChangeAnimSpeed(float speed)
        {
            if (speed < _speedToRun && speed > 0)
                _animator.speed = GetAnimSpeed(speed,_speedToRun);

            else if (speed >= _speedToRun)
                _animator.speed = GetAnimSpeed(speed,_speedCom.MaxSpeed);

            else if (speed == 0)
                _animator.speed = 1;
        }
        private void ChangeParticles(float speed)
        {
            var t = speed / _speedCom.MaxSpeed;

            var emission = _dirtParticle.emission;
            emission.rateOverTime = _emissionByMaxSpeed*t;

            var main = _dirtParticle.main;
            main.startSpeed=_startSpeedByMaxSpeed*t;
            
        }
    }
}