using UnityEngine;
using System.Reflection;

[CreateAssetMenu(fileName = "NewFoodData", menuName = "FoodData", order = 0)]
public class FoodData : ScriptableObject
{
    [SerializeField] private Type _type;
    [SerializeField] private Color _color;
    [SerializeField] private Shape _shape;
}
