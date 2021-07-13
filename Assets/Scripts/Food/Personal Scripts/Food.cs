using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(FoodAnimation))]
public class Food : MonoBehaviour
{
    // делегат для вызова сравнения
    readonly Comparer comparer = new Comparer(FoodComparer.Compare);

    //посадить на свойства
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
        Debug.Log(comparer(this));
    }
}
