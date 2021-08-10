using UnityEngine;

public class Mover : NeedConstruction
{
    public static bool NeedOneTimeStop = true;
    private Road _road;
    private SpeedComponent _speedCom;
    private Stopper _stopper;

    private new void OnEnable()
    {
        base.OnEnable();
        CorrectEvent.OnAction += CorrectPosition;
    }
    private new void OnDisable()
    {
        base.OnDisable();
        CorrectEvent.OnAction -= CorrectPosition;
    }


    private void Start()
    {
        _speedCom = GetComponent<SpeedComponent>();
        _road = FindObjectOfType<Road>();
        _stopper = FindObjectOfType<Stopper>();
    }
    private void FixedUpdate()
    {
        if (!_speedCom.IsStop && !Player.IsGoToFood)
        {
            Move(-_speedCom.Speed * Time.deltaTime);
        }
    }
    private void Move(float step)
    {
        if (_constraction != null)
            _constraction.transform.Translate(0,0,step);

        if (_road != null)
            _road.transform.Translate(0, 0, step);

    }

    public void CorrectPosition(float delta)
    {
        Move(delta);
    }
}
