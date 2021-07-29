using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalCamera : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    private GameObject _constraction;
    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        _constraction = FoodSpawner.Construction;
        if (_constraction!=null)
        {
            transform.position = _constraction.transform.position + _offset;
        }
        if (Distance.Value>20)
        {
            _camera.enabled = true;
        }
        else
            _camera.enabled = false;
    }

}
