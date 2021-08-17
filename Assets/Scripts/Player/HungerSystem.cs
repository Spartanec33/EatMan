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

        public float Satiety => _satiety;
        public float MaxSatiety => _maxSatiety;

        private void OnEnable()
        {
            SatietyChangedEvent.OnAction += Validate;
        }
        private void OnDisable()
        {
            SatietyChangedEvent.OnAction -= Validate;
        }
        private void Start()
        {
            AddSatiety(_maxSatiety);
        }
        private void FixedUpdate()
        {
            if (Player.IsDied != true)
            {
                AddHunger();
            }
        }
        public void AddSatiety()
        {
            var value = Random.Range(_minSatietyWhenEating, _maxSatietyWhenEating);
            _satiety += value;
            SatietyChangedEvent.ActivateEvent();
        }
        public void AddSatiety(float value)
        {
            _satiety += value;
            SatietyChangedEvent.ActivateEvent();
        }
        private void AddHunger()
        {
            _satiety -= _hungerForAdd;
            SatietyChangedEvent.ActivateEvent();
        }

        private void Validate()
        {
            if (_satiety < 0)
            {
                DieEvent.ActivateEvent();
                _satiety = 0;
            }
            else if (_satiety > _maxSatiety)
                _satiety = _maxSatiety;
        }
    }
}
