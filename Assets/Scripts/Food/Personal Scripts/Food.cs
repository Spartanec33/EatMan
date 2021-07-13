using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(FoodAnimation))]
public class Food : MonoBehaviour
{
    public enum Type
    {
        Fruit,
        Vegetable,
        Meat,
        Bread,
        Milk,
        Multy,
    }
    public enum Color
    {
        Red,
        Green,
        Yellow,
        Blue,
        White,
        Black,
        Brown,
    }
    public enum Shape
    {
        Square,
        Circle,
        Sphere,
        Arc,
        Shapeless,
    }

    [SerializeField]
    protected Type type;
    [SerializeField]
    protected Color color;
    [SerializeField]
    protected Shape shape;


    private void OnMouseDown()
    {
        if (FoodFinal.isCoroutineActive==false)
            StartCoroutine(FoodFinal.Final(this));
    }

}
