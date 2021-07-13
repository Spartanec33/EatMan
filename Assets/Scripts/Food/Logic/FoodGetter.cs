using UnityEngine;
using System.Reflection;
using System;

public static class FoodGetter
{
    private static readonly FoodListData foodListData = GameObject.FindObjectOfType<FoodListData>();
    private static readonly Food[] foods = GetFoods();
    private static readonly System.Random random = new System.Random();

    public static Food GetRandomFood() => foods[random.Next(0, foods.Length)];
    public static Food[] GetFoods() => foodListData.GetListData;

    public static string[] GetProperties(Food food)
    {
        FieldInfo[] fields = food.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic) ;
        string[] res = new string[fields.Length];
        for (int i = 0; i < fields.Length; i++)
            res[i] = fields[i].GetValue(food).ToString();
        return res;
    }

    //берет рандомное колличество свойст еды в случайном порядке 
    public static  string[] GetRandomProperties(Food food)
    {
        string[] fields = GetProperties(food);
        string[] answer = new string[random.Next(1, fields.Length + 1)];

        int[] IntermediateArray = new int[answer.Length];

        for (int i = 0; i < IntermediateArray.Length; i++)
            IntermediateArray[i] = -2;


        //заполнение промежуточного массива
        for (int i = 0; i < IntermediateArray.Length; i++)
        {
            bool complete = false;

            while (complete != true)
            {
                int a = random.Next(0, fields.Length);
                if (Array.IndexOf(IntermediateArray, a) == -1)
                {
                    IntermediateArray[i] = a;
                    complete = true;
                }
            }
        }

        //заполнение итогового массива с помощью индексов из промежуточного
        for (int i = 0; i < answer.Length; i++)
            answer[i] = fields[IntermediateArray[i]];
        return answer;
    }

}
