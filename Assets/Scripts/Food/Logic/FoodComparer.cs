using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


public static class FoodComparer
{

    public static Food TargetFood { get; set; }
    public static string[] TargetProperties { get; set; }

    
    public static bool Compare(Food clickedFood)
    {
        
        string[] clickedProperties = FoodGetter.GetProperties(clickedFood);

        for (int i = 0; i < TargetProperties.Length; i++)
        {
            if (Array.IndexOf(clickedProperties, TargetProperties[i]) == -1)
                return false;
        }
        return true;
    }
}

