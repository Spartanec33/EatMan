using System.Collections;
using UnityEngine;
using UseEvents;
using UseMove;

namespace UseUIComponents.FoodView
{
    [RequireComponent(typeof(FoodViewClick))]
    public class FoodViewMover : MonoBehaviour
    {
        [SerializeField] private float _timeForMove;
        [SerializeField] private float _distanceToHide;

        private Vector3 _startPos;
        private Vector3 _finishPos;
        private bool _isCoroutineActive = false;
        private readonly WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
        private Distance _distance;
        private FoodViewClick _foodView;

        private void OnEnable()
        {
            ChangeResolutionEvent.OnAction += InitMovePos;
        }
        private void OnDisable()
        {
            ChangeResolutionEvent.OnAction -= InitMovePos;
        }
        private void Start()
        {
            _distance = FindObjectOfType<Distance>();
            _foodView = GetComponent<FoodViewClick>();
        }

        private void Update()
        {
            var distance = _distance.Value;
            ViewMover(distance);
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
            float progress = 0;
            Vector3 viewPos = transform.position;
            while (progress <= 1)
            {
                yield return _waitForFixedUpdate;
                progress = (coveredDistance / allWay);
                transform.position = Vector3.Lerp(viewPos, finishPos, progress);
                coveredDistance += (speed * Time.deltaTime);
            }
            if (progress >= 1)
                transform.position = Vector3.Lerp(viewPos, finishPos, 1);
        }
        private void InitMovePos()
        {
            var startPosX = (_foodView.RectTrans.anchorMin.x + ((_foodView.RectTrans.anchorMax.x - _foodView.RectTrans.anchorMin.x) / 2)) * _foodView.Canvas.pixelRect.width;
            var startPosY = (_foodView.RectTrans.anchorMin.y + ((_foodView.RectTrans.anchorMax.y - _foodView.RectTrans.anchorMin.y) / 2)) * _foodView.Canvas.pixelRect.height;
            _startPos = new Vector3(startPosX, startPosY);

            var anchorDelta = _foodView.RectTrans.anchorMax.y - _foodView.RectTrans.anchorMin.y;
            _finishPos = new Vector3(_startPos.x, _foodView.Canvas.pixelRect.height + anchorDelta * _foodView.Canvas.pixelRect.height * 2);

            if (_isCoroutineActive == true)
                transform.position = _finishPos;
        }
    }
}