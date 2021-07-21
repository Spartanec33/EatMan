using UnityEngine;

public class Mover : MonoBehaviour
{
    public static bool WasOneTimeStop;
    public static bool NeedOneTimeStop = true;
    [SerializeField] private float _speed;
    [SerializeField] private float _maxSpeed;

    private GameObject _constraction;
    private Road _road;

    public  float Speed { get => _speed; set => _speed = value; }
    public float MaxSpeed { get => _maxSpeed; }

    private void Start()
    {
        _road = GameObject.FindObjectOfType<Road>();
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void RoadMove()
    {
        _road.transform.Translate(0, 0, -_speed * Time.deltaTime);
    }
    private void FoodMove()
    {
        if (_constraction!=null)
            _constraction.transform.Translate(0, 0, -_speed * Time.deltaTime);
        else
            _constraction = FoodSpawner.Construction;
    }
    public void Move()
    {
            RoadMove();
            FoodMove();
    }
}
