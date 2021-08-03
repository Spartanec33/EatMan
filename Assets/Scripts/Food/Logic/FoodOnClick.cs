using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodOnClick: MonoBehaviour
{
    [SerializeField] private GameObject _targetParticle;
    [SerializeField] private float _coroutineStep = 0.05f;
    [SerializeField] private float _newStopDistance = 2;
    [SerializeField] private float _lengthReturnPath = 15;

    private static Coroutine _cor;
    private List<Coroutine> _coroutines;
    private bool _isCoroutineActive;
    private bool _canChangeTarget;
    private Food _oldFood;

    private readonly WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
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

    public IEnumerator Final(Food food)
    {
        CreateTargetParticle(food);

        _oldFood = food;
        _isCoroutineActive = true;
        _canChangeTarget = true;
        Mover.NeedOneTimeStop = false;

        yield return StartCoroutineUsingAdapter(ChangeTransform(food));

        _canChangeTarget = false;

        //�������� ��������

        if (FoodComparer.Compare(food))
        {
            FoodSpawner.Delete();
            FoodSpawner.Spawn();
            _hungerSystem.AddSatiety();
        }

        //�������� �� ����� ���������


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


    //������ ��������� ������ � ���
    private IEnumerator ChangeTransform(Food food)
    {
        yield return StartCoroutineUsingAdapter(Rotate(GetFinalRotationToFood(food)));
        yield return StartCoroutineUsingAdapter(Move(food.transform.position, Distance.Value - _newStopDistance));

    }

    //���������� ������ �� �����
    private IEnumerator UndoTransform()
    {
        yield return StartCoroutineUsingAdapter(Rotate(_basePlayerRotation));
        yield return StartCoroutineUsingAdapter(Move(_basePlayerPosition, _lengthReturnPath));
    }


    private IEnumerator Move(Vector3 endPoint, float allWay)
    {
        float coveredDistance = 0;
        Vector3 playerPos = _player.transform.position;
        while (coveredDistance <= allWay)
        {
            yield return _waitForFixedUpdate;
            var progress = (coveredDistance / allWay);
            var posX = Vector3.Lerp(playerPos, endPoint, progress).x;
            _player.transform.position = new Vector3(posX, _player.transform.position.y, _player.transform.position.z);

            coveredDistance += (_speedCom.Speed * Time.deltaTime);
        }
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
    private Quaternion GetFinalRotationToFood(Food food)
    {
        Quaternion startRotation = _player.transform.rotation;
        _player.transform.LookAt(food.transform);
        Quaternion finalRotation = _player.transform.rotation;
        finalRotation = new Quaternion(_basePlayerRotation.x, finalRotation.y, _basePlayerRotation.z, _basePlayerRotation.w);
        _player.transform.rotation = startRotation;
        return finalRotation;
    }
    public void OnClick(Food food)
    {
        if (food!=null && food!=_oldFood)
        {
            if (_isCoroutineActive==false || _canChangeTarget)
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
