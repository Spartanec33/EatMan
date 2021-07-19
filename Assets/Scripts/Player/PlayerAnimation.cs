using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Mover mover;
    private Animator animator;

    private void Start()
    {
        mover = GameObject.FindObjectOfType<Mover>();
        animator = GameObject.FindObjectOfType<Player>().GetComponent<Animator>();
    }
    private void Update()
    {
        //�������� �� �������
        animator.SetFloat("Speed", mover.Speed);

    }
}
