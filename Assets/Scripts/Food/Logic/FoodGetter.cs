using UnityEngine;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Collections;
using UseFoodComponent.ForInspector;
using UseFoodComponent.Personal;

namespace UseFoodComponent.Logic
{
    public static class FoodGetter
    {
        private static readonly FoodListData _foodListData = GameObject.FindObjectOfType<FoodListData>();
        private static readonly Food[] _foods = GetFoods();
        private static readonly System.Random _random = new System.Random();
        public static Food TargetFood { get; private set; }
        public static string[] TargetProperties { get; private set; }

        public static Food GetRandomFood() => _foods[_random.Next(0, _foods.Length)];
        public static Food[] GetFoods() => _foodListData.GetListData;
        public static List<string> GetProperties(Food food)
        {
            FoodPropertiesData foodData = food.FoodData;

            //массив полей
            FieldInfo[] fields = foodData.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            //лист массивов
            List<string> res = new List<string>(5);

            for (int i = 0; i < fields.Length; i++)
            {
                IEnumerable Arr = fields[i].GetValue(foodData) as IEnumerable;
                foreach (var item in Arr)
                    res.Add(item.ToString());
            }
            return res;
        }
        public static string[] GetRandomProperties(Food food)
        {
            List<string> AllPropertyies = GetProperties(food);
            string[] answer = new string[_random.Next(1, AllPropertyies.Count + 1)];
            int[] intermediateArray = new int[answer.Length];

            for (int i = 0; i < intermediateArray.Length; i++)
                intermediateArray[i] = -2;


            //заполнение промежуточного массива
            for (int i = 0; i < intermediateArray.Length; i++)
            {
                while (true)
                {
                    int index = _random.Next(0, AllPropertyies.Count);
                    if (Array.IndexOf(intermediateArray, index) == -1)
                    {
                        intermediateArray[i] = index;
                        break;
                    }
                }
            }

            //заполнение итогового массива с помощью индексов из промежуточного
            for (int i = 0; i < answer.Length; i++)
                answer[i] = AllPropertyies[intermediateArray[i]];
            return answer;
        }
        public static void ChooseFood()
        {
            TargetFood = GetRandomFood();
            TargetProperties = GetRandomProperties(TargetFood);
        }

    }
}
