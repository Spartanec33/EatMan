using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalCamera : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private GameObject _view;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _exitPosition;

    private GameObject _constraction;
    private Vector3 _startPos;
    private bool _isCoroutineActive = false;
    private readonly WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();


    private void Start()
    {
        _startPos = _view.transform.position;
    }

    private void Update()
    {
        _constraction = FoodSpawner.Construction;
        if (_constraction!=null)
        {
            transform.position = _constraction.transform.position + _offset;
        }


        if (Distance.Value < 50 && _isCoroutineActive == false)
            StartCoroutine(Move(_view.transform.position + _exitPosition));
        else if ((Distance.Value > 50 && _isCoroutineActive == true))
        {
            StartCoroutine(Move(_startPos));
            _isCoroutineActive = false;
        }

    }

    private IEnumerator Move(Vector3 endPoint)
    {
        _isCoroutineActive = true;
        float allWay = Vector3.Distance(_view.transform.position, endPoint);
        float coveredDistance = 0;
        Vector3 viewPos = _view.transform.position;
        while (coveredDistance <= allWay)
        {
            yield return _waitForFixedUpdate;
            var progress = (coveredDistance / allWay);
            var pos = Vector3.Lerp(viewPos, endPoint, progress);
            _view.transform.position = pos;
            coveredDistance += (_speed * Time.deltaTime);
        }
    }

}
