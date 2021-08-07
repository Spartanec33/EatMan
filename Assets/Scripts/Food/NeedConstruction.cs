using UnityEngine;


//юзать для классов, которым нужна конструкция с едой и ее обновление
public class NeedConstruction : MonoBehaviour
{
    protected GameObject _constraction;
    protected void OnEnable() => ChangeConstructionEvent.OnAction += ChangeConstruction;
    protected void OnDisable() => ChangeConstructionEvent.OnAction += ChangeConstruction;
    void ChangeConstruction() => _constraction = FoodSpawner.Construction;
}