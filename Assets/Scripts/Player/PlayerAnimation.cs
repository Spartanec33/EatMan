using UnityEngine;


[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private SpeedComponent _speedCom;
    private Animator _animator;

    private void Start()
    {
        _speedCom = GameObject.FindObjectOfType<SpeedComponent>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        //посадить на событие
        _animator.SetFloat("Speed", _speedCom.Speed);

    }
}
