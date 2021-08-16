using UnityEngine;
using System.Reflection;
using System;
using UseFoodComponent.ForInspector;
using UseFoodComponent.Personal;

namespace UseFoodComponent.Logic
{
    public static class FoodGetter
    {
        private static readonly FoodListData _foodListData = GameObject.FindObjectOfType<FoodListData>();
        private static readonly Food[] _foods = GetFoods();
        private static readonly System.Random _random = new System.Random();

        public static Food GetRandomFood() => _foods[_random.Next(0, _foods.Length)];
        public static Food[] GetFoods() => _foodListData.GetListData;
        public static string[] GetProperties(Food food)
        {
            FoodPropertiesData foodData = food.FoodData;
            FieldInfo[] fields = foodData.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            string[] res = new string[fields.Length];
            for (int i = 0; i < fields.Length; i++)
                res[i] = fields[i].GetValue(foodData).ToString();
            return res;
        }
        public static string[] GetRandomProperties(Food food)
        {
            string[] fields = GetProperties(food);
            string[] answer = new string[_random.Next(1, fields.Length + 1)];
            int[] intermediateArray = new int[answer.Length];

            for (int i = 0; i < intermediateArray.Length; i++)
                intermediateArray[i] = -2;


            //заполнение промежуточного массива
            for (int i = 0; i < intermediateArray.Length; i++)
            {
                while (true)
                {
                    int index = _random.Next(0, fields.Length);
                    if (Array.IndexOf(intermediateArray, index) == -1)
                    {
                        intermediateArray[i] = index;
                        break;
                    }
                }
            }

            //заполнение итогового массива с помощью индексов из промежуточного
            for (int i = 0; i < answer.Length; i++)
                answer[i] = fields[intermediateArray[i]];
            return answer;
        }

    }
}
