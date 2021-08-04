using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodView : NeedConstruction
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _exitPosition;

    private Vector3 _startPos;
    private bool _isCoroutineActive = false;
    private readonly WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

    private void Start()
    {
        _startPos = transform.position;
    }
    private void Update()
    {
        
        if (_constraction!=null)
        {
            _camera.transform.position = _constraction.transform.position + _offset;
        }


        if (Distance.Value < 50 && _isCoroutineActive == false)
            StartCoroutine(Move(transform.position + _exitPosition));
        else if ((Distance.Value > 50 && _isCoroutineActive == true))
        {
            StartCoroutine(Move(_startPos));
            _isCoroutineActive = false;
           
        }

    }

    private IEnumerator Move(Vector3 endPoint)
    {
        _isCoroutineActive = true;
        float allWay = Vector3.Distance(transform.position, endPoint);
        float coveredDistance = 0;
        Vector3 viewPos = transform.position;
        while (coveredDistance <= allWay)
        {
            yield return _waitForFixedUpdate;
            var progress = (coveredDistance / allWay);
            var pos = Vector3.Lerp(viewPos, endPoint, progress);
            transform.position = pos;
            coveredDistance += (_speed * Time.deltaTime);
        }
    }
}
