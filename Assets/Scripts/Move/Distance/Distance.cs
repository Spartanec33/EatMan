using System;
using UnityEngine;

public static class Distance
{
    private static GameObject _constraction;
    private static Player _player = GameObject.FindObjectOfType<Player>();
    public static float Value { get; private set; }

    public static void DistanceUpdate()
    {
        if (FoodSpawner.Construction != null)
        {
            _constraction = FoodSpawner.Construction;
            Value = _constraction.transform.position.z - _player.transform.position.z;
        }
        else
            Value = 9999;
    }
}
