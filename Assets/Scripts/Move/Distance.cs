using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance : NeedConstruction
{
    private  Player _player;
    public float Value => DistanceUpdate();
    private void Start()
    {
        _player = GameObject.FindObjectOfType<Player>();
    }

    public float DistanceUpdate()
    {
        if (_constraction != null)
            return _constraction.transform.position.z - _player.transform.position.z;
        else
            return  9999;
    }
}
