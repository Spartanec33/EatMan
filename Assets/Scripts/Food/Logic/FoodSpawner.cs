﻿using System.Collections;
using UnityEngine;

public static class FoodSpawner 
{
    private static FoodSpawnData data;
    private static readonly InspectorFoodSpawnData inspectorData = GameObject.FindObjectOfType<InspectorFoodSpawnData>();
    //родительский обьект для упрощения манипуляций с малышами
    private static GameObject construction;

    private static readonly Food[] foods = FoodGetter.GetFoods();


    //выбрать правильную еду и получить ее рандомные свойства
    private static void ChooseFood()
    {
        FoodComparer.TargetFood = FoodGetter.GetRandomFood();
        FoodComparer.TargetProperties = FoodGetter.GetRandomProperties(FoodComparer.TargetFood);
    }
    public static void Spawn()
    {
        if (construction==null)
        {
            ChooseFood();
            int placeForTargetFood = Random.Range(0, data.numberOfPieces);

            construction=CreateConstuction();

            Quaternion rotation = construction.transform.rotation;
            Vector3 position = construction.transform.position;

            for (int i = 0; i < data.numberOfPieces; i++)
            {
                float placeX = position.x+data.offset*i;
                Vector3 place = new Vector3(placeX, position.y, position.z);

                if (i!=placeForTargetFood)
                {
                    var randomFood = foods[Random.Range(0, foods.Length)];
                    var food=GameObject.Instantiate(randomFood,place,rotation);
                    food.transform.SetParent(construction.transform);
                }

                else
                {
                    var food = GameObject.Instantiate(FoodComparer.TargetFood, place, rotation);
                    food.transform.SetParent(construction.transform);
                }
            }
        }
    }
    public static void Delete()
    {
        if (construction != null)
            GameObject.Destroy(construction);
    }
    private static GameObject CreateConstuction()
    {
        var construction = new GameObject();
        construction.transform.SetParent(GameObject.FindObjectOfType<Road>().transform);
        data = inspectorData.data;
        construction.transform.SetPositionAndRotation(new Vector3(data.constructionPosition.x-data.numberOfPieces - 1, data.constructionPosition.y,
                                                      data.constructionPosition.z), data.constructionRotation);
        return construction;
    }
}