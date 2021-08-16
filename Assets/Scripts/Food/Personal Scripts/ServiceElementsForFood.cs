using UnityEngine;

namespace UseFoodComponent.Personal
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

    public class ServiceElementsForFood : MonoBehaviour
    {
        protected Animator _animator;
    }
}