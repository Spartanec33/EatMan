using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance : NeedConstruction
{
    private  Player _player;
    public static float Value { get; private set; }
    private void Start()
    {
        _player = GameObject.FindObjectOfType<Player>();
    }

    public void DistanceUpdate()
    {
        if (_constraction != null)
            Value = _constraction.transform.position.z - _player.transform.position.z;
        else
            Value = 9999;
    }
    private void FixedUpdate()
    {
        DistanceUpdate();
    }
}
