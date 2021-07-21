using UnityEngine;

public class InspectorFoodSpawnData : MonoBehaviour
{
    [SerializeField] private FoodSpawnData _data;
    public FoodSpawnData Data => _data;
}

