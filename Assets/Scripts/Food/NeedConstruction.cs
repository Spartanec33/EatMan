using UnityEngine;
using UseEvents;
using UseFoodComponent.Logic;

namespace UseFoodComponent.NeedConstruction
{
    //юзать для классов, которым нужна конструкция с едой и ее обновление
    public class NeedConstruction : MonoBehaviour
    {
        protected GameObject _constraction;
        
        protected void OnEnable() => OnChangeConstruction.OnAction += ChangeConstruction;
        protected void OnDisable() => OnChangeConstruction.OnAction += ChangeConstruction;
        void ChangeConstruction() => _constraction = FoodSpawner.Construction;
    }
}