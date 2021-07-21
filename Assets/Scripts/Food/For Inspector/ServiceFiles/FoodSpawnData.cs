using UnityEngine;
using System;

[Serializable]
public struct FoodSpawnData
{
    [SerializeField] private float _offset;
    [SerializeField] private int _numberOfPieces;
    [SerializeField] private FoodTransformSpawnData _transform;
    [SerializeField] private RuntimeAnimatorController _animController;

    public float Offset => _offset;
    public int NumberOfPieces => _numberOfPieces;

    public Vector2 ConstructionPosition => _transform.ConstructionPosition; 
    public float ZMinPosition => _transform.ZMinPosition;
    public float ZMaxPosition => _transform.ZMaxPosition;
    public Quaternion ConstructionRotation => _transform.ConstructionRotation;
    public RuntimeAnimatorController AnimController => _animController;
}

