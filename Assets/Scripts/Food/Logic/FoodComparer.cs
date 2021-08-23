using System;
using UnityEngine;
using System.Collections.Generic;
using UseFoodComponent.Personal;

namespace UseFoodComponent.Logic
{
    public class FoodComparer:MonoBehaviour
    {
        public static bool Compare(Food clickedFood)
        {

            List<string> AllPropertiesInFood = FoodGetter.GetProperties(clickedFood);

            for (int i = 0; i < FoodGetter.TargetProperties.Length; i++)
            {
                if (AllPropertiesInFood.IndexOf(FoodGetter.TargetProperties[i]) == -1)
                    return false;
            }
            return true;
        }
    }
}

