using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerSystem : MonoBehaviour
{
    [SerializeField] private float _maxHunger;
    [SerializeField] private float _hungerForAdd;

    private float _hunger;
    private int myVar;

    public int MyProperty
    {
        get { return myVar; }
        set { myVar = value; }
    }


    public void AddSatiety(float value) => _hunger += value;

    private void AddHunger() => _hunger -= _hungerForAdd;
}
