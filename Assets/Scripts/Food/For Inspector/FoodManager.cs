using UnityEngine;
using UseFoodComponent.Logic.OnClick;

namespace UseFoodComponent.ForInspector
{
    [RequireComponent(typeof(FoodListData))]
    [RequireComponent(typeof(FoodOnClickController))]
    [RequireComponent(typeof(InspectorFoodSpawnData))]
    public class FoodManager : MonoBehaviour
    {
    }
}