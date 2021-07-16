using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;



//анимация еды
public class FoodAnimation : MonoBehaviour
{
    private bool upward=true;
    private int quantity;
    private int currentQuantity;
    public FoodAnimationData animationData=new FoodAnimationData();


    private void Start()
    {
        animationData.Initialize();
        quantity = (int)(animationData.verticalRange / animationData.verticalSpeed);
    }

    private void FixedUpdate()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        transform.Rotate(0, animationData.horizontalSpeed, 0);
    }
    private void Move()
    {
        int direction = upward ? 1 : -1;
        transform.position+=new Vector3(0,direction* animationData.verticalSpeed, 0);
        currentQuantity++;
        if (currentQuantity==quantity)
        {
            currentQuantity = 0;
            upward=!upward;
        }
        
    }
}
