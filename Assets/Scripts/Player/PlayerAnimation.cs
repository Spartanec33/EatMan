using UnityEngine;


[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Mover _mover;
    private Animator _animator;

    private void Start()
    {
        _mover = GameObject.FindObjectOfType<Mover>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        //�������� �� �������
        _animator.SetFloat("Speed", _mover.Speed);

    }
}
