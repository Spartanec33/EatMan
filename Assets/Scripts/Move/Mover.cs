using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private RoadRepeater roadRepeater;
    private Road road;

    private Player player;
    private GameObject constraction;

    [SerializeField] private float speed = 50;
    [SerializeField] private float stopDistance=10;
    public static bool isMovable;

    private void Start()
    {
        roadRepeater = GameObject.FindObjectOfType<RoadRepeater>();
        road = GameObject.FindObjectOfType<Road>();
        player = GameObject.FindObjectOfType<Player>();
    }

    private void FixedUpdate()
    {
        CheckDistance();
        if (isMovable)
        {
            Move();
        }
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
            isMovable = distance >= stopDistance;
        }
        else constraction = FoodSpawner.construction;
    }
    public float FindDistance()
    {
        return constraction.transform.position.z - player.transform.position.z;
    }
    
}
