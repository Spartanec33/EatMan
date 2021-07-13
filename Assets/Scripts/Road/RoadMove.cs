using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMove : MonoBehaviour
{

    private Vector3 startPos;
    private float repeatWidth;
    private float speed = 50;
    private void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.z/2;
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        if(transform.position.z<startPos.z-repeatWidth)
            transform.position = startPos;
        transform.Translate(0,0,-speed*Time.deltaTime);
        //git
    }

}
