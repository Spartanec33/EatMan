using UnityEngine;
using System;

namespace UseFoodComponent.ForInspector.Service
{
    [Serializable]
    public struct FoodTransformSpawnData
    {
        public Vector2 ConstructionPosition;
        public float ZMinPosition;
        public float ZMaxPosition;
        public Quaternion ConstructionRotation;
    }
}

