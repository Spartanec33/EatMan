using UnityEngine;

[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(HungerSystem))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody[] rigidbodies;

    private SpeedComponent _speedcom;

    private Animator _animator;
    private void Start()
    {
        _speedcom = FindObjectOfType<SpeedComponent>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToRagdoll();
        }
    }
    private void ToRagdoll()
    {
        _animator.enabled = false;
        foreach (var item in rigidbodies)
        {
            item.isKinematic = false;
            item.velocity = new Vector3(0, 0, _speedcom.Speed / 10);
        }
        _speedcom.Stop();
    }
}
