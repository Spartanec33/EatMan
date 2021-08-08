using UnityEngine;

public class Mover : NeedConstruction
{
    public static bool NeedOneTimeStop = true;
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
    public void Move()
    {
        if (!_speedCom.IsStop && !Player.IsGoToFood)
        {
            if (_constraction != null)
                IndividualMove(_constraction.transform);
            
            if (_road != null)
                IndividualMove(_road.transform);
        }
    }
    private void IndividualMove(Transform obj) => obj.Translate(0, 0, -_speedCom.Speed * Time.deltaTime);
}
