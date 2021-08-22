using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UseEvents;

namespace UsePlayerComponents
{
    public class HungerSystem : MonoBehaviour
    {
        [SerializeField] private float _maxSatiety;
        [SerializeField] private float _hungerForAdd;
        [SerializeField] private float _minSatietyWhenEating;
        [SerializeField] private float _maxSatietyWhenEating;
        [SerializeField] private float _satiety;

        public float Satiety
        {
            get 
            {
                return _satiety;
            }
            private set 
            {
                _satiety = value;
                if (_satiety < 0)
                {
                    OnDie.ActivateEvent();
                    _satiety = 0;
                }
                else if (_satiety > _maxSatiety)
                    _satiety = _maxSatiety;
            }
        }

        public float MaxSatiety => _maxSatiety;

        private void Start()
        {
            AddSatiety(_maxSatiety);
        }
        private void FixedUpdate()
        {
            if (Player.IsDied != true && GameState.IsStarted != false)
            {
                AddHunger();
            }
        }
        public void AddSatiety()
        {
            var value = Random.Range(_minSatietyWhenEating, _maxSatietyWhenEating);
            Satiety += value;
            OnSatietyChanged.ActivateEvent();
        }
        public void AddSatiety(float value)
        {
            Satiety += value;
            OnSatietyChanged.ActivateEvent();
        }
        private void AddHunger()
        {
            Satiety -= _hungerForAdd;
            OnSatietyChanged.ActivateEvent();
        }

    }
}
