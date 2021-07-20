using UnityEngine;
using System;

[Serializable]
public struct FoodTransformSpawnData
{
    public Vector2 ConstructionPosition;
    public float ZMinPosition;
    public float ZMaxPosition;
    public Quaternion ConstructionRotation;
}

[Serializable]
public struct FoodSpawnData
{
    [SerializeField] private float _offset;
    [SerializeField] private int _numberOfPieces;
    [SerializeField] private FoodTransformSpawnData _transform;

    public float Offset => _offset;
    public int NumberOfPieces => _numberOfPieces;

    public Vector2 ConstructionPosition => _transform.ConstructionPosition; 
    public float ZMinPosition => _transform.ZMinPosition;
    public float ZMaxPosition => _transform.ZMaxPosition;
    public Quaternion ConstructionRotation => _transform.ConstructionRotation;
}

public class InspectorFoodSpawnData : MonoBehaviour
{
    [SerializeField] private FoodSpawnData _data;
    public FoodSpawnData Data => _data;
}

