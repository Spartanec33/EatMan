using UnityEngine;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Collections;
using UseFoodComponent.ForInspector;
using UseFoodComponent.Personal;

namespace UseFoodComponent.Logic
{
    public class FoodGetter: MonoBehaviour
    {
        [SerializeField] private int _maxNumberOfTargetProperties ;

        private FoodListData _foodListData;
        private Food[] _foods;
        private System.Random _random = new System.Random();

        public static Food TargetFood { get; private set; }
        public static string[] TargetProperties { get; private set; }


        private void Awake()
        {
            _foodListData = GetComponent<FoodListData>();
            _foods = GetFoods();
            TargetFood = null;
            TargetProperties = null;
        }
        public Food GetRandomFood() => _foods[_random.Next(0, _foods.Length)];
        public Food[] GetFoods()
        {
            return _foodListData.GetListData;
        }

        public static List<string> GetProperties(Food food)
        {
            FoodPropertiesData foodData = food.FoodData;

            FieldInfo[] fields = foodData.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            List<string> res = new List<string>(5);

            for (int i = 0; i < fields.Length; i++)
            {
                IEnumerable Arr = fields[i].GetValue(foodData) as IEnumerable;
                foreach (var item in Arr)
                    res.Add(item.ToString());
            }
            return res;
        }
        public string[] GetRandomProperties(Food food)
        {
            List<string> AllPropertyies = GetProperties(food);

            int TargetPropertiesAmount;
            if (AllPropertyies.Count > _maxNumberOfTargetProperties)
                TargetPropertiesAmount = _random.Next(1, _maxNumberOfTargetProperties + 1);

            else
                TargetPropertiesAmount = _random.Next(1, AllPropertyies.Count + 1);

            string[] answer = new string[TargetPropertiesAmount];
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
        public void ChooseFood()
        {
            TargetFood = GetRandomFood();
            TargetProperties = GetRandomProperties(TargetFood);
        }

    }
}
