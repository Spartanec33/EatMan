using UnityEngine;




public class Food : MonoBehaviour
{
    private FoodOnClick onClick;
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

    public Type type;
    public Color color;
    public Shape shape;

    private void Start()
    {
        onClick = GameObject.FindObjectOfType<FoodOnClick>();
    }
    private void OnMouseDown()
    {
        if (FoodOnClick.isCoroutineActive==false)
            StartCoroutine(onClick.Final(this));
    }

}
