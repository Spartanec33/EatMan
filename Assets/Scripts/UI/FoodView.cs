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
    private bool _isCoroutineActive = false;
    private Distance _distance;
    private readonly WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

    private void Start()
    {
        _distance = FindObjectOfType<Distance>();
        _startPos = transform.position;
        var RectTrans = gameObject.GetComponent<RectTransform>();
        var anchorDelta = RectTrans.anchorMax.y - RectTrans.anchorMin.y;
        _finishPos = new Vector3(_startPos.x, _canvas.pixelRect.height + anchorDelta * _canvas.pixelRect.height);
    }
    private void LateUpdate()
    {
        var distance = _distance.Value;
        if (_constraction != null)
        {
            _camera.transform.position = _constraction.transform.position + _offset;
        }

        if (distance < _distanceToHide && _isCoroutineActive == false)
            StartCoroutine(Move(_finishPos));
        else if ((distance > _distanceToHide && _isCoroutineActive == true))
        {
            StartCoroutine(Move(_startPos));
            _isCoroutineActive = false;

        }
        _texture.Release();
        _texture.width = (int)gameObject.GetComponent<RawImage>().rectTransform.rect.width;
        _texture.height = (int)gameObject.GetComponent<RawImage>().rectTransform.rect.height;

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
            var pos = Vector3.Lerp(viewPos, finishPos, progress);
            transform.position = pos;
            coveredDistance += (speed * Time.deltaTime);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var pressPosition = eventData.pressPosition;
        Debug.Log(pressPosition);
        pressPosition = Convert(pressPosition);
        Debug.Log(pressPosition);

        Ray ray = _camera.ScreenPointToRay(pressPosition);
        Debug.Log(new Vector2(_camera.scaledPixelWidth,_camera.scaledPixelHeight));

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere), hit.point, new Quaternion());
            if (hit.collider.GetComponent<Food>())
            {
                Food food = hit.collider.GetComponent<Food>();
                if (!Player.IsDied)
                    FoodClickEvent.ActivateEvent(food);

            }

        }
        Vector2 Convert(Vector2 pressPosition)
        {
            var RectTrans = gameObject.GetComponent<RectTransform>();
            var minA = RectTrans.anchorMin.y;
            var cHeight = _canvas.pixelRect.height;
            return new Vector2(pressPosition.x, pressPosition.y - cHeight * minA);
        }
    }
}
