using UnityEngine;

public class Mover : MonoBehaviour
{
    public static bool NeedOneTimeStop = true;

    private GameObject _constraction;
    private Road _road;
    private Camera _additionalCamera;
    private SpeedComponent _speedCom;


    private void Start()
    {
        _speedCom = GetComponent<SpeedComponent>();
        _road = FindObjectOfType<Road>();
    }
    private void FixedUpdate()
    {
        _constraction = FoodSpawner.Construction;
        Move();
    }
    public void Move()
    {
        if (!_speedCom.IsStop)
        {
            if (_constraction != null)
                IndividualMove(_constraction.transform);
            
            if (_road != null)
                IndividualMove(_road.transform);
        }
    }
    private void IndividualMove(Transform obj) => obj.Translate(0, 0, -_speedCom.Speed * Time.deltaTime);
}
