using UnityEngine;


public class ServiceElementsForFood: MonoBehaviour
{
    protected FoodOnClick _onClick;
}
public class Food : ServiceElementsForFood
{
    public enum Type : byte
    {
        Fruit,
        Vegetable,
        Meat,
        Bread,
        Milk,
        Multy,
    }
    public enum Color : byte
    {
        Red,
        Green,
        Yellow,
        Blue,
        White,
        Black,
        Brown,
    }
    public enum Shape : byte
    {
        Square,
        Circle,
        Sphere,
        Arc,
        Shapeless,
    }

    [SerializeField] private Type _type;
    [SerializeField] private Color _color;
    [SerializeField] private Shape _shape;

    public void Init(FoodOnClick onClick)
    {
        _onClick = onClick;
    }
    private void OnMouseDown()
    {
        if (FoodOnClick.IsCoroutineActive == false)
            StartCoroutine(_onClick.Final(this));
    }


}
