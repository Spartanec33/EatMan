using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Food : ServiceElementsForFood
{
    [SerializeField] private Type _type;
    [SerializeField] private Color _color;
    [SerializeField] private Shape _shape;

    public void Init(FoodOnClick onClick, RuntimeAnimatorController controller)
    {
        _onClick = onClick;
        _animator = GetComponent<Animator>();
        _animator.runtimeAnimatorController = controller;
        _animator.applyRootMotion = true;

    }

    private void OnMouseDown()
    {
        _onClick.OnClick(this);
    }
}
