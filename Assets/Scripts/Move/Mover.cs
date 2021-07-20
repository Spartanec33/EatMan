using UnityEngine;

public class Mover : MonoBehaviour
{
    public static bool OneTimeStop;
    public static bool NeedOneTimeStop = true;

    [SerializeField] private float _speed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _speedPerClick;
    [SerializeField] private float _speedReduction;
    [SerializeField] float _baseSpeedReduction;
    [SerializeField] private float _stopDistance = 10;

    private RoadRepeater _roadRepeater;
    private Road _road;
    private Player _player;
    private GameObject _constraction;

    public float Speed => _speed;

    private void Start()
    {
        _roadRepeater = GameObject.FindObjectOfType<RoadRepeater>();
        _road = GameObject.FindObjectOfType<Road>();
        _player = GameObject.FindObjectOfType<Player>();
    }

    private void FixedUpdate()
    {
        ReduceSpeed();
        CheckDistance();
        Move();
    }


    private void RoadMove()
    {
        _roadRepeater.TryRepeat();
        _road.transform.Translate(0, 0, -_speed * Time.deltaTime);
    }
    private void FoodMove()
    {
        if (_constraction!=null)
            _constraction.transform.Translate(0, 0, -_speed * Time.deltaTime);
        else
            _constraction = FoodSpawner._construction;
    }
    public void Move()
    {
            RoadMove();
            FoodMove();
    }


    private void CheckDistance()
    {
        if (_constraction != null && _road != null)
        {
            var distance = FindDistance();
            if (distance <= _stopDistance && OneTimeStop == false && NeedOneTimeStop == true)
            {
                _speed = 0;

                if (FoodOnClick.IsCoroutineActive==true)
                    OneTimeStop = true;
                
            }
        }
        else _constraction = FoodSpawner._construction;
    }
    public float FindDistance()
    {
        return _constraction.transform.position.z - _player.transform.position.z;
    }

    public void AddSpeedPerClick() => _speed += _speedPerClick;
    private void ReduceSpeed()
    {
        _speedReduction = (_speed / _maxSpeed) * 0.1f + _baseSpeedReduction;
        if (_speed > 0)
            _speed -= _speedReduction;
        else if(_speed < 0)
            _speed = 0;
        if (_speed > 0 && _speed < 0.3f)
            _speed = 0;
    }
}
