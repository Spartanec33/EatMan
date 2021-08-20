using System;
using UseFoodComponent.Personal;

namespace UseFoodComponent.Logic
{
    public static class FoodComparer
    {
        public static bool Compare(Food clickedFood)
        {

            string[] clickedProperties = FoodGetter.GetProperties(clickedFood);

            for (int i = 0; i < FoodGetter.TargetProperties.Length; i++)
            {
                if (Array.IndexOf(clickedProperties, FoodGetter.TargetProperties[i]) == -1)
                    return false;
            }
            return true;
        }
    }
}

