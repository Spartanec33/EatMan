using System;
using UnityEngine;

public static class DistanceFinder
{
    private static GameObject _constraction;
    private static Player _player = GameObject.FindObjectOfType<Player>();
    public static float Find()
    {
        if (FoodSpawner.Construction != null)
        {
            _constraction = FoodSpawner.Construction;
            return _constraction.transform.position.z - _player.transform.position.z;
        }
        return 9999;
    }
}
