using UnityEngine;
using System;

[Serializable]
public struct FoodSpawnData
{
    public float offset;
    public int numberOfPieces;
    public Vector2 constructionPosition;
    public float zMinPosition;
    public float zMaxPosition;
    public Quaternion constructionRotation;
}

public class InspectorFoodSpawnData : MonoBehaviour
{
    public FoodSpawnData data;
}

