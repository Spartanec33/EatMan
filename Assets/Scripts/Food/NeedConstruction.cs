﻿using UnityEngine;


//юзать для классов, которым нужна конструкция с едой и ее обновление
public class NeedConstruction : MonoBehaviour
{
    protected GameObject _constraction;
    protected void OnEnable() => EventStorage.ChangeConstructionEvent.Action += ChangeConstruction;
    protected void OnDisable() => EventStorage.ChangeConstructionEvent.Action += ChangeConstruction;
    void ChangeConstruction() => _constraction = FoodSpawner.Construction;
}