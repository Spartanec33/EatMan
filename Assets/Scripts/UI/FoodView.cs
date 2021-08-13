using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FoodView : NeedConstruction, IPointerDownHandler
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Camera _camera;
    [SerializeField] private RenderTexture _texture;
    [SerializeField] private float _timeForMove;
    [SerializeField] private float _distanceToHide;
    [SerializeField] private Canvas _canvas;

    private Vector3 _startPos;
    private Vector3 _finishPos;

    private RectTransform _rectTrans;
    private RawImage _rawImage;
    private Vector2 _canvasOld;

    private bool _isCoroutineActive = false;
    private Distance _distance;
    private readonly WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

    private void Start()
    {
        _distance = FindObjectOfType<Distance>();
        _rectTrans = GetComponent<RectTransform>();
        _rawImage = GetComponent<RawImage>();
        InitMovePos();
    }
    private void LateUpdate()
    {
        var distance = _distance.Value;

        CameraMove();
        ViewMover(distance);
        TryChangeResolution();
    }

    private void CameraMove()
    {
        if (_constraction != null)
        {
            _camera.transform.position = _constraction.transform.position + _offset;
        }
    }

    private void ViewMover(float distance)
    {
        if (distance < _distanceToHide && _isCoroutineActive == false)
            StartCoroutine(Move(_finishPos));
        else if ((distance > _distanceToHide && _isCoroutineActive == true))
        {
            StartCoroutine(Move(_startPos));
            _isCoroutineActive = false;

        }
    }

    private IEnumerator Move(Vector3 finishPos)
    {
        _isCoroutineActive = true;
        float allWay = Vector3.Distance(transform.position, finishPos);
        float speed = allWay / _timeForMove;
        float coveredDistance = 0;
        Vector3 viewPos = transform.position;
        while (coveredDistance <= allWay)
        {
            yield return _waitForFixedUpdate;
            var progress = (coveredDistance / allWay);
            if (progress > 1)
                progress = 1;
            transform.position = Vector3.Lerp(viewPos, finishPos, progress);
            coveredDistance += (speed * Time.deltaTime);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var pressPosition = Convert(eventData.pressPosition);
        Ray ray = _camera.ScreenPointToRay(pressPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.GetComponent<Food>())
            {
                Food food = hit.collider.GetComponent<Food>();
                if (!Player.IsDied)
                    FoodClickEvent.ActivateEvent(food);
            }
        }
    }

    private Vector2 Convert(Vector2 pressPosition)
    {
        var minA = _rectTrans.anchorMin.y;
        var cHeight = _canvas.pixelRect.height;
        return new Vector2(pressPosition.x, pressPosition.y - cHeight * minA);
    }

    private void TryChangeResolution()
    {
        Vector2 canvasNow = new Vector2(_canvas.pixelRect.width, _canvas.pixelRect.height);
        if (_canvasOld != canvasNow)
        {
            _texture.Release();
            _texture.width = (int)_rawImage.rectTransform.rect.width;
            _texture.height = (int)_rawImage.rectTransform.rect.height;
            _canvasOld = canvasNow;
        }
    }
    private void InitMovePos()
    {
        _startPos = transform.position;
        var anchorDelta = _rectTrans.anchorMax.y - _rectTrans.anchorMin.y;
        _finishPos = new Vector3(_startPos.x, _canvas.pixelRect.height + anchorDelta * _canvas.pixelRect.height);
    }
}
