using System.Collections;
using UnityEngine;
using UseEvents;
using UseFoodComponent.Personal;
using UseMove;
using UsePlayerComponents;

namespace UseFoodComponent.Logic.OnClick
{
    public class FoodOnClickFunctions : MonoBehaviour
    {
        [SerializeField] private GameObject _targetParticle;
        [SerializeField] private float _coroutineStep = 0.05f;
        [SerializeField] private float _newStopDistance = 2;
        [SerializeField] private float _lengthReturnPath = 15;
        [Range(0,360)][SerializeField] private float _pukeAngle;

        private Player _player;
        private FoodOnClickController _controller;
        private Distance _distance;
        private SpeedComponent _speedCom;
        private GameObject _spawnedTargetParticle;
        private Vector3 _basePlayerPosition;
        private Quaternion _basePlayerRotation;

        private bool _moveToFoodCorIsActive;
        private readonly WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

        public static bool IsGoToFood { get; private set; }

        private void Start()
        {
            _speedCom = FindObjectOfType<SpeedComponent>();
            _player = FindObjectOfType<Player>();
            _controller = GetComponent<FoodOnClickController>();
            _distance = FindObjectOfType<Distance>();

            _basePlayerPosition = _player.transform.position;
            _basePlayerRotation = _player.transform.rotation;
            IsGoToFood = false;
        }
        public void CreateTargetParticle(Food food)
        {
            if (_spawnedTargetParticle != null)
                Destroy(_spawnedTargetParticle);
            _spawnedTargetParticle = Instantiate(_targetParticle, food.transform);
        }

        //меняет положение игрока к еде
        public IEnumerator ChangeTransform(Food food)
        {
            _controller.StartCoroutineAndAddToList(Rotate(GetFinalRotation(food.transform.position)));
            yield return _controller.StartCoroutineAndWaitIt(MoveToFood(food));

        }

        //возвращает игрока на место
        public IEnumerator UndoTransform()
        {
            _controller.StartCoroutineAndAddToList(Rotate(_basePlayerRotation));
            yield return _controller.StartCoroutineAndWaitIt(Move(_basePlayerPosition, _lengthReturnPath));
        }

        private IEnumerator MainPartMove(float allWay, Vector3 endPoint, bool NeedCorrectPos = false)
        {
            float progress = 0;
            float coveredDistance = 0;
            Vector3 startPlayerPos = _player.transform.position;
            var playerPos = _player.transform.position;

            while (progress <= 1)
            {
                yield return _waitForFixedUpdate;
                coveredDistance += (_speedCom.Speed * Time.deltaTime);
                progress = (coveredDistance / allWay);
                var posX = Vector3.Lerp(startPlayerPos, endPoint, progress).x;
                _player.transform.position = new Vector3(posX, playerPos.y, playerPos.z);
            }
            if (progress>1)
            {
                var posX = Vector3.Lerp(startPlayerPos, endPoint, 1).x;
                _player.transform.position = new Vector3(posX, playerPos.y, playerPos.z);
            }
        }
        private IEnumerator Move(Vector3 endPoint, float allWay)
        {
            yield return _controller.StartCoroutineAndWaitIt(MainPartMove(allWay, endPoint));
        }
        private IEnumerator MoveToFood(Food food)
        {
            _moveToFoodCorIsActive = true;
            var endPoint = food.transform.position;
            _controller.StartCoroutineAndAddToList(CheckPlayerGoingToFood(endPoint));
            float allWay = Mathf.Abs(Vector3.Distance(_player.transform.position, endPoint) - _newStopDistance);
            yield return _controller.StartCoroutineAndWaitIt(MainPartMove(allWay, endPoint, true));
            _moveToFoodCorIsActive = false;
        }
        private IEnumerator CheckPlayerGoingToFood(Vector3 endPoint)
        {
            var wasCorrect = false;
            while (_moveToFoodCorIsActive)
            {
                yield return _waitForFixedUpdate;

                if (_distance.Value <= _newStopDistance)
                {
                    if(!wasCorrect)
                        OnCorrect.ActivateEvent(_newStopDistance - _distance.Value);
                    IsGoToFood = true;
                }
                else
                    IsGoToFood = false;
            }
            IsGoToFood = false;
        }

        public IEnumerator Rotate(Quaternion finalRotation)
        {
            float progress = 0;
            var startRotation = _player.transform.rotation;
            while (progress <= 1)
            {
                yield return _waitForFixedUpdate;
                _player.transform.rotation = Quaternion.Lerp(startRotation, finalRotation, progress);
                progress += _coroutineStep;
            }
            if (progress>=1)
                _player.transform.rotation = finalRotation;
            
        }
        private Quaternion GetFinalRotation(Vector3 point)
        {
            Quaternion startRotation = _player.transform.rotation;
            _player.transform.LookAt(point);
            Quaternion finalRotation = _player.transform.rotation;
            finalRotation = new Quaternion(_basePlayerRotation.x, finalRotation.y, _basePlayerRotation.z, _basePlayerRotation.w);
            _player.transform.rotation = startRotation;
            return finalRotation;
        }
        public IEnumerator RotateToVomit()
        {
            
            var direction = _player.transform.position.x < 0 / 2 ? 1 : -1;
            var rotation = Quaternion.Euler(transform.rotation.x, _pukeAngle * direction, transform.rotation.z);
            yield return _controller.StartCoroutineAndWaitIt(Rotate(rotation));
        }
    }
}
