using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodOnClick: MonoBehaviour
{
    [SerializeField] private GameObject _targetParticle;
    [SerializeField] private float _coroutineStep = 0.05f;
    [SerializeField] private float _newStopDistance = 2;
    [SerializeField] private float _lengthReturnPath = 15;

    private Coroutine _cor;
    private List<Coroutine> _coroutines;
    private readonly WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

    private bool _isCoroutineActive;
    private bool _canChangeTarget;
    private Food _oldFood;

    private SpeedComponent _speedCom;
    private HungerSystem _hungerSystem;

    private Player _player;
    private GameObject _spawnedTargetParticle;
    private Vector3 _basePlayerPosition;
    private Quaternion _basePlayerRotation;
    

    private void Start()
    {
        _player = GameObject.FindObjectOfType<Player>();
        _speedCom = GameObject.FindObjectOfType<SpeedComponent>();
        _hungerSystem = GameObject.FindObjectOfType<HungerSystem>();

        _coroutines = new List<Coroutine>(10);
        _basePlayerPosition = _player.transform.position;
        _basePlayerRotation = _player.transform.rotation;
    }
    private void OnEnable()
    {
        FoodClickEvent.OnAction += OnClick;
        DieEvent.OnAction += StopAllCoroutinesOfThisClass;
    }
    private void OnDisable()
    {
        FoodClickEvent.OnAction -= OnClick;
        DieEvent.OnAction -= StopAllCoroutinesOfThisClass;
    }

    public IEnumerator Final(Food food)
    {
        CreateTargetParticle(food);

        _oldFood = food;
        _isCoroutineActive = true;
        _canChangeTarget = true;
        Mover.NeedOneTimeStop = false;
        yield return StartCoroutineUsingAdapter(ChangeTransform(food));


        //анимация поедания

        if (FoodComparer.Compare(food)!=true)
        {
            Destroy(food.gameObject);
            _canChangeTarget = false;
            yield return StartCoroutineUsingAdapter(_player.Puke());
        }

        _canChangeTarget = true; 
        while (FoodComparer.Compare(food) != true)
        {
            _speedCom.Stop();
            yield return _waitForFixedUpdate;
        }
        _canChangeTarget = false;

        FoodSpawner.Delete();
        FoodSpawner.Spawn();
        _hungerSystem.AddSatiety();
        
        //действия по итогу сравнения


        _isCoroutineActive = false;
        yield return StartCoroutineUsingAdapter(UndoTransform());
        Mover.NeedOneTimeStop = true;
    }
    private void CreateTargetParticle(Food food)
    {
        if (_spawnedTargetParticle != null)
            Destroy(_spawnedTargetParticle);
        _spawnedTargetParticle = Instantiate(_targetParticle, food.transform);
    }
    
    //меняет положение игрока к еде
    private IEnumerator ChangeTransform(Food food)
    {
        yield return StartCoroutineUsingAdapter(Rotate(GetFinalRotation(food.transform.position)));
        yield return StartCoroutineUsingAdapter(Move(food.transform.position));

    }

    //возвращает игрока на место
    private IEnumerator UndoTransform()
    {
        yield return StartCoroutineUsingAdapter(Rotate(_basePlayerRotation));
        yield return StartCoroutineUsingAdapter(Move(_basePlayerPosition, _lengthReturnPath));
    }


    private IEnumerator Move(Vector3 endPoint, float allWay)
    {
        float coveredDistance = 0;
        Vector3 startPlayerPos = _player.transform.position;
        while (coveredDistance <= allWay)
        {
            var playerPos = _player.transform.position;
            yield return _waitForFixedUpdate;
            var progress = (coveredDistance / allWay);
            var posX = Vector3.Lerp(startPlayerPos, endPoint, progress).x;
            _player.transform.position = new Vector3(posX, playerPos.y, playerPos.z);
            coveredDistance += (_speedCom.Speed * Time.deltaTime);
        }
    }
    private IEnumerator Move(Vector3 endPoint)
    {
        float coveredDistance = 0;
        Vector3 startPlayerPos = _player.transform.position;
        float allWay = Vector3.Distance(startPlayerPos, endPoint) - _newStopDistance;
        while (coveredDistance <= allWay)
        {
            var playerPos = _player.transform.position;
            yield return _waitForFixedUpdate;

            if (Mathf.Abs(playerPos.z - endPoint.z) <= _newStopDistance)
                Player.IsGoToFood = true;

            var progress = (coveredDistance / allWay);
            if (progress>1)
                progress = 1;
            
            var posX = Vector3.Lerp(startPlayerPos, endPoint, progress).x;
            _player.transform.position = new Vector3(posX, playerPos.y, playerPos.z);
            coveredDistance += (_speedCom.Speed * Time.deltaTime);
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

    public void OnClick(Food food)
    {
        if (food != null && food != _oldFood)
        {
            if (_isCoroutineActive == false || _canChangeTarget)
            {
                StopAllCoroutinesOfThisClass();
                StartCoroutineAndAddToList(Final(food));
            }
        }
    }





    private void StopAllCoroutinesOfThisClass()
    {
        foreach (var item in _coroutines)
        {
            if (item != null)
                StopCoroutine(item);
        }
        _coroutines.Clear();
    }
    private IEnumerator StartCoroutineUsingAdapter(IEnumerator coroutine)
    {
        StartCoroutineAndAddToList(coroutine);
        yield return _cor;
    }
    private void StartCoroutineAndAddToList(IEnumerator coroutine) => _coroutines.Add(_cor = StartCoroutine(coroutine));
}
