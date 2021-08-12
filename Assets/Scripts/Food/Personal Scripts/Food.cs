using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Food : ServiceElementsForFood
{
    [SerializeField] private FoodData _foodData;

    public FoodData FoodData => _foodData;

    public void Init(RuntimeAnimatorController controller)
    {
        _animator = GetComponent<Animator>();
        _animator.runtimeAnimatorController = controller;
        _animator.applyRootMotion = true;
        GetComponent<BoxCollider>().isTrigger = true;
    }

    public void Eat()
    {
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        if (!Player.IsDied)
            FoodClickEvent.ActivateEvent(this);
    }

}
