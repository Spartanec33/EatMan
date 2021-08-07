using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerSystem : MonoBehaviour
{
    [SerializeField] private float _maxSatiety;
    [SerializeField] private float _hungerForAdd;
    [SerializeField] private float _minSatietyWhenEating;
    [SerializeField] private float _maxSatietyWhenEating;

    [SerializeField] private float _satiety;
    public float Satiety
    {
        get { return _satiety; }
        private set { _satiety = value; }
    }

    private void Start()
    {
        AddSatiety(_maxSatiety);
    }
    private void FixedUpdate()
    {

        AddHunger();
        Validate();
    }
    public void AddSatiety()
    {
        var value = Random.Range(_minSatietyWhenEating, _maxSatietyWhenEating);
        Satiety += value;
    }
    public void AddSatiety(float value)
    {
        Satiety += value;
    }
    private void AddHunger() => Satiety -= _hungerForAdd;
    private void Validate()
    {
        if (Satiety < 0 && !Player.Died)
            DieEvent.ActivateEvent();
        else if (Satiety > _maxSatiety)
            Satiety = _maxSatiety;
    }
}
