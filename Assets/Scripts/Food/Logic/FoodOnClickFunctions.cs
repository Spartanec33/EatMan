using System.Collections;
using UnityEngine;

public class FoodOnClickFunctions : MonoBehaviour
{
    [SerializeField] private GameObject _targetParticle;
    [SerializeField] private float _coroutineStep = 0.05f;
    [SerializeField] private float _newStopDistance = 2;
    [SerializeField] private float _lengthReturnPath = 15;

    private Player _player;
    private FoodOnClickController _controller;
    private SpeedComponent _speedCom;
    private GameObject _spawnedTargetParticle;
    private Vector3 _basePlayerPosition;
    private Quaternion _basePlayerRotation;

    private bool _moveToFoodCorIsActive;
    private readonly WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

    private void Start()
    {
        _speedCom = FindObjectOfType<SpeedComponent>();
        _player = FindObjectOfType<Player>();
        _controller = FindObjectOfType<FoodOnClickController>();

        _basePlayerPosition = _player.transform.position;
        _basePlayerRotation = _player.transform.rotation;
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
        yield return _controller.StartCoroutineAndWaitIt(MoveToFood(food.transform.position));

    }

    //возвращает игрока на место
    public IEnumerator UndoTransform()
    {
        _controller.StartCoroutineAndAddToList(Rotate(_basePlayerRotation));
        yield return _controller.StartCoroutineAndWaitIt(Move(_basePlayerPosition, _lengthReturnPath));
    }

    private IEnumerator MainPartMove(float allWay, Vector3 endPoint)
    {
        float coveredDistance = 0;
        Vector3 startPlayerPos = _player.transform.position;

        while (coveredDistance <= allWay)
        {
            yield return _waitForFixedUpdate;

            var playerPos = _player.transform.position;
            var progress = (coveredDistance / allWay);

            if (progress > 1)
                progress = 1;

            var posX = Vector3.Lerp(startPlayerPos, endPoint, progress).x;
            _player.transform.position = new Vector3(posX, playerPos.y, playerPos.z);
            coveredDistance += (_speedCom.Speed * Time.deltaTime);
        }
    }
    private IEnumerator Move(Vector3 endPoint, float allWay)
    {
        yield return _controller.StartCoroutineAndWaitIt(MainPartMove(allWay, endPoint));
    }
    private IEnumerator MoveToFood(Vector3 endPoint)
    {
        _moveToFoodCorIsActive = true;

        _controller.StartCoroutineAndAddToList(CheckPlayerGoingToFood(endPoint));
        float allWay = Vector3.Distance(_player.transform.position, endPoint) - _newStopDistance;
        yield return _controller.StartCoroutineAndWaitIt(MainPartMove(allWay, endPoint));

        _moveToFoodCorIsActive = false;
    }
    private IEnumerator CheckPlayerGoingToFood(Vector3 endPoint)
    {
        while (_moveToFoodCorIsActive)
        {
            yield return _waitForFixedUpdate;

            if (Mathf.Abs(_player.transform.position.z - endPoint.z) <= _newStopDistance)
                Player.IsGoToFood = true;
        }
        Player.IsGoToFood = false;
    }

    private IEnumerator Rotate(Quaternion finalRotation)
    {
        float progress = 0;
        var startRotation = _player.transform.rotation;
        while (progress < 1)
        {
            yield return _waitForFixedUpdate;
            _player.transform.rotation = Quaternion.Lerp(startRotation, finalRotation, progress);
            progress += _coroutineStep;
        }
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
}
