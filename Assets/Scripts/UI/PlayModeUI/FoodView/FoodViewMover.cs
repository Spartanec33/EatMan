using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UseEvents;
using UseMove;
using UsePlayerComponents;

namespace UseUIComponents.FoodView
{
    public class FoodViewMover : MonoBehaviour
    {
        [SerializeField] private float _fractionPerSecond;
        [SerializeField] private float _distanceToHide;

        private Vector3 _startPos;
        private Vector3 _finishPos;
        private Camera _camera;

        private bool _isGoToStart;
        private readonly WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
        private Distance _distance;
        private FoodViewClick _foodView;
        private Coroutine _moveCor;

        private void OnEnable()
        {
            OnChangeResolution.OnAction += InitMovePos;
            OnStartButtonClick.OnAction += Allow;
            OnDie.OnAction += OffCamera;
        }
        private void OnDisable()
        {
            OnChangeResolution.OnAction -= InitMovePos;
            OnStartButtonClick.OnAction -= Allow;
            OnDie.OnAction -= OffCamera ;
        }
        private void Start()
        {
            _distance = FindObjectOfType<Distance>();
            _foodView = GetComponent<FoodViewClick>();
            _camera = _foodView.Camera;
            _camera.gameObject.SetActive(false);
            InitMovePos();
            _foodView.transform.position = _startPos;
        }

        private void Update()
        {           
            ViewMover();
        }

        private void ViewMover()
        {
            var distance = _distance.Value;
            if (distance < _distanceToHide && _isGoToStart == false)
            {
                if (_moveCor != null)
                    StopCoroutine(_moveCor);
                _moveCor = StartCoroutine(Move(_startPos, true));
                _isGoToStart = true;
            }
            else if (distance > _distanceToHide && _isGoToStart)
            {
                if (_moveCor!=null)
                    StopCoroutine(_moveCor);
                _moveCor = StartCoroutine(Move(_finishPos));
                _isGoToStart = false;
            }
        }
        private IEnumerator Move(Vector3 finishPos, bool needOffCameraAfterMove = false)
        {
            OnCamera();
            
            float allWay = Vector3.Distance(transform.position, finishPos);
            float speed = _foodView.Canvas.pixelRect.height * _fractionPerSecond;
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

            if (needOffCameraAfterMove)
                OffCamera();
            
        }
        private void InitMovePos()
        {
            var finishPosX = (_foodView.RectTrans.anchorMin.x + ((_foodView.RectTrans.anchorMax.x - _foodView.RectTrans.anchorMin.x) / 2)) * _foodView.Canvas.pixelRect.width;
            var finishPosY = (_foodView.RectTrans.anchorMin.y + ((_foodView.RectTrans.anchorMax.y - _foodView.RectTrans.anchorMin.y) / 2)) * _foodView.Canvas.pixelRect.height;
            _finishPos = new Vector3(finishPosX, finishPosY);

            var anchorDelta = _foodView.RectTrans.anchorMax.y - _foodView.RectTrans.anchorMin.y;
            _startPos = new Vector3(_finishPos.x, _foodView.Canvas.pixelRect.height + anchorDelta * _foodView.Canvas.pixelRect.height);
        }
        private void Allow()
        {
            _moveCor = StartCoroutine(Move(_finishPos));
        }
        private void OffCamera()
        {
            _camera.gameObject.SetActive(false);
        }
        private void OnCamera()
        {
            if (_camera.isActiveAndEnabled == false)
                _camera.gameObject.SetActive(true);
        }
    }
}