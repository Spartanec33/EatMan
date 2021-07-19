using UnityEngine;

public class Mover : MonoBehaviour
{
    private RoadRepeater roadRepeater;

    private Road road;
    private Player player;
    private GameObject constraction;

    [SerializeField] private float speed;
    public float Speed => speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float speedPerClick;
    [SerializeField] private float speedReduction;
    [SerializeField] float baseSpeedReduction;


    [SerializeField] private float stopDistance=10;

    public static bool oneTimeStop;
    public static bool needOneTimeStop=true;

    private void Start()
    {
        roadRepeater = GameObject.FindObjectOfType<RoadRepeater>();
        road = GameObject.FindObjectOfType<Road>();
        player = GameObject.FindObjectOfType<Player>();
    }

    private void FixedUpdate()
    {
        ReduceSpeed();
        CheckDistance();
        Move();
    }


    private void RoadMove()
    {
        roadRepeater.TryRepeat();
        road.transform.Translate(0,0,-speed*Time.deltaTime);
    }
    private void FoodMove()
    {
        if (constraction!=null)
            constraction.transform.Translate(0, 0, -speed * Time.deltaTime);
        else
            constraction = FoodSpawner.construction;
    }
    public void Move()
    {
            RoadMove();
            FoodMove();
    }


    private void CheckDistance()
    {
        if(constraction!=null && road!=null)
        {
            var distance = FindDistance();
            if (distance <= stopDistance && oneTimeStop==false && needOneTimeStop==true)
            {
                speed = 0;

                if (FoodOnClick.isCoroutineActive==true)
                    oneTimeStop = true;
                
            }
        }
        else constraction = FoodSpawner.construction;
    }
    public float FindDistance()
    {
        return constraction.transform.position.z - player.transform.position.z;
    }

    public void AddSpeedPerClick() => speed += speedPerClick;
    private void ReduceSpeed()
    {
        speedReduction = (speed / maxSpeed)*0.1f+baseSpeedReduction;
        if (speed > 0)
            speed -= speedReduction;
        else if(speed < 0)
            speed = 0;
        if (speed > 0 && speed < 0.3f)
            speed = 0;
    }
}
