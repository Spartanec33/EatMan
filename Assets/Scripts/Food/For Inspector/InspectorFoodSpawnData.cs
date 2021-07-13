using System.Collections;
using UnityEngine;
using System;

[Serializable]
public struct FoodSpawnData
{
    public float offset;
    public int numberOfPieces;
    public Vector3 constructionPosition;
    public Quaternion constructionRotation;
}

public class InspectorFoodSpawnData : MonoBehaviour
{
    public FoodSpawnData data;
}

