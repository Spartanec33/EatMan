using System.Collections;
using UnityEngine;

public class FoodOnClick: MonoBehaviour
{
    public static bool IsCoroutineActive;

    [SerializeField] private float _coroutineStep = 0.05f;
    [SerializeField] private float _newStopDistance = 2;
    [SerializeField] private float _lengthReturnPath = 15;

    private readonly WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

    private Player _player;
    private Mover _mover;

    private Vector3 _basePlayerPosition;
    private Quaternion _basePlayerRotation;


    private void Start()
    {
        _player = GameObject.FindObjectOfType<Player>();
        _mover = GameObject.FindObjectOfType<Mover>();

        _basePlayerPosition = _player.transform.position;
        _basePlayerRotation = _player.transform.rotation;
    }

    public  IEnumerator Final(Food food)
    {
        IsCoroutineActive = true;
        Mover.NeedOneTimeStop = false;

        yield return StartCoroutine(Rotate(food));
        yield return StartCoroutine(Move(food));        
        
        //анимация

        // сравнение
        if (FoodComparer.Compare(food) && _mover.FindDistance() <= _newStopDistance)
        {
            FoodSpawner.Delete();
            FoodSpawner.Spawn();
        }
        

        

        IsCoroutineActive = false;
        Mover.OneTimeStop = false;
        Mover.NeedOneTimeStop = true;
        yield return StartCoroutine(Undo());
    }




    public IEnumerator Rotate(Food food)
    {
        _player.transform.LookAt(food.transform);
        var finalRotation = _player.transform.rotation;
        finalRotation = new Quaternion(_basePlayerRotation.x, finalRotation.y, _basePlayerRotation.z, _basePlayerRotation.w);
        float progress = 0;
        while (progress < 1)
        {
            _player.transform.rotation = Quaternion.Lerp(_basePlayerRotation, finalRotation, progress);
            yield return _waitForFixedUpdate;
            progress += _coroutineStep;
        }
    }
    public IEnumerator Move(Food food)
    {
        float allWay = _mover.FindDistance() - _newStopDistance;
        float coveredDistance = 0;

        var foodPos = food.transform.position;
        var playerPos = _player.transform.position;
        while (_mover.FindDistance() >= _newStopDistance)
        {

            yield return _waitForFixedUpdate;

            var progress = (coveredDistance / allWay);
            var posX = Vector3.Lerp(playerPos, foodPos, progress).x;
            _player.transform.position = new Vector3(posX, _player.transform.position.y, _player.transform.position.z);

            if (Mover.OneTimeStop==true)
                _mover.Move();
            coveredDistance += (_mover.Speed * Time.deltaTime);
        }
    }

    //возвращает игрока на место
    public IEnumerator Undo()
    {
        yield return StartCoroutine(UndoRotation());
        yield return StartCoroutine(UndoPosition());
    }
    public IEnumerator UndoRotation()
    {
        float progress = 0;
        var startRotation = _player.transform.rotation;
        while (progress < 1)
        {
            _player.transform.rotation = Quaternion.Lerp(startRotation, _basePlayerRotation, progress);
            yield return _waitForFixedUpdate;
            progress += _coroutineStep;
        }
    }
    public IEnumerator UndoPosition()
    {
        float coveredDistance = 0;

        var playerPos = _player.transform.position;
        while (coveredDistance <= _lengthReturnPath)
        {

            yield return _waitForFixedUpdate;

            var progress = (coveredDistance / _lengthReturnPath);
            var posX = Vector3.Lerp(playerPos, _basePlayerPosition, progress).x;
            _player.transform.position = new Vector3(posX, _player.transform.position.y, _player.transform.position.z);

            coveredDistance += (_mover.Speed * Time.deltaTime);
        }
    }
}
