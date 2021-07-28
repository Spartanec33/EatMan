using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalCamera : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    private GameObject _constraction;

    private void LateUpdate()
    {
        _constraction = FoodSpawner.Construction;
        if (_constraction!=null)
        {
            transform.position = _constraction.transform.position - _offset;
        }
        
    }
}
