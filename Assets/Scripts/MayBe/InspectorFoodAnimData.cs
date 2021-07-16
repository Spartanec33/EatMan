using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public struct FoodAnimationData
{
    public float verticalRange;
    public float verticalSpeed;
    public float horizontalSpeed;
    public void Initialize()
    {
        var inspectorFoodAnimData = GameObject.FindObjectOfType<InspectorFoodAnimData>().animData;
        verticalRange = inspectorFoodAnimData.verticalRange;
        verticalSpeed = inspectorFoodAnimData.verticalSpeed;
        horizontalSpeed = inspectorFoodAnimData.horizontalSpeed;
    }
}
public class InspectorFoodAnimData: MonoBehaviour
{

    public FoodAnimationData animData;

}


