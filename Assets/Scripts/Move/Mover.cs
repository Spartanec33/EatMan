using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private RoadRepeater roadRepeater;
    private Road road;

    private Player player;
    private GameObject constraction;

    public float speed = 50;
    public float stopDistance=10;
    public static bool isMovable;

    private void Start()
    {
        roadRepeater = GameObject.FindObjectOfType<RoadRepeater>();
        road = GameObject.FindObjectOfType<Road>();
        player = GameObject.FindObjectOfType<Player>();
    }

    private void FixedUpdate()
    {
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
    private void Move()
    {
        if(isMovable)
        {
            RoadMove();
            FoodMove();
        }
        CheckDistance();
    }
    private void CheckDistance()
    {
        if(constraction!=null && road!=null)
        {
            var distance = Vector3.Distance(constraction.transform.position, player.transform.position);
            isMovable = distance >= stopDistance;
        }
        else constraction = FoodSpawner.construction;
    }
    
}
