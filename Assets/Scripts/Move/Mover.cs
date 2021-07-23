using UnityEngine;

public class Mover : MonoBehaviour
{
    public static bool WasOneTimeStop;
    public static bool NeedOneTimeStop = true;

    private GameObject _constraction;
    private Road _road;
    private SpeedComponent _speedCom;


    private void Start()
    {
        _speedCom = GetComponent<SpeedComponent>();
        _road = FindObjectOfType<Road>();
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void RoadMove()
    {
        _road.transform.Translate(0, 0, -_speedCom.Speed * Time.deltaTime);
    }
    private void FoodMove()
    {
        if (_constraction!=null)
            _constraction.transform.Translate(0, 0, -_speedCom.Speed * Time.deltaTime);
        else
            _constraction = FoodSpawner.Construction;
    }
    public void Move()
    {
            RoadMove();
            FoodMove();
    }
}
