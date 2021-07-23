using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//нужно прописывать условия для выхода каждой корутине
public class FoodOnClick: MonoBehaviour
{

    [SerializeField] private float _coroutineStep = 0.05f;
    [SerializeField] private float _newStopDistance = 2;
    [SerializeField] private float _lengthReturnPath = 15;

    private static Coroutine _cor;
    private List<Coroutine> _coroutines;
    private static bool _isCoroutineActive;
    private bool _canChangeTarget;

    private readonly WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
    private SpeedComponent _speedCom;

    private Player _player;
    private Vector3 _basePlayerPosition;
    private Quaternion _basePlayerRotation;

    public static bool IsCoroutineActive => _isCoroutineActive;


    private void Start()
    {
        _player = GameObject.FindObjectOfType<Player>();
        _speedCom = GameObject.FindObjectOfType<SpeedComponent>();

        _coroutines = new List<Coroutine>(10);
        _basePlayerPosition = _player.transform.position;
        _basePlayerRotation = _player.transform.rotation;
    }

    public IEnumerator Final(Food food)
    {

        _isCoroutineActive = true;
        _canChangeTarget = true;
        Mover.NeedOneTimeStop = false;

        yield return StartCoroutineUsingAdapter(ChangeTransform(food));

        _canChangeTarget = false;

        //анимация поедания

        if (FoodComparer.Compare(food))
        {
            FoodSpawner.Delete();
            FoodSpawner.Spawn();
        }

        //действия по итогу сравнения

        _isCoroutineActive = false;
        Mover.NeedOneTimeStop = true;

        yield return StartCoroutineUsingAdapter(UndoTransform());
    }


    //меняет положение игрока к еде
    private IEnumerator ChangeTransform(Food food)
    {
        yield return StartCoroutineUsingAdapter(Rotate(GetFinalRotationToFood(food)));
        yield return StartCoroutineUsingAdapter(Move(food.transform.position, DistanceFinder.Find() - _newStopDistance));

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
        if (food!=null)
        {
            if (IsCoroutineActive==false || _canChangeTarget)
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
