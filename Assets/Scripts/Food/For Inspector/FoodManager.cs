using UnityEngine;
using UseFoodComponent.Logic;
using UseFoodComponent.Logic.OnClick;

namespace UseFoodComponent.ForInspector
{
    [RequireComponent(typeof(FoodListData))]
    [RequireComponent(typeof(FoodOnClickController))]
    [RequireComponent(typeof(FoodSpawner))]
    [RequireComponent(typeof(FoodGetter))]
    [RequireComponent(typeof(FoodComparer))]
    public class FoodManager : MonoBehaviour
    {
    }
}