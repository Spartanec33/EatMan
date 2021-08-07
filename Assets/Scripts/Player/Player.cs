using UnityEngine;

[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(HungerSystem))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody[] rigidbodies;

    private static bool _died;
    private SpeedComponent _speedcom;
    private Animator _animator;

    public static bool Died => _died;

    private void Start()
    {
        _speedcom = FindObjectOfType<SpeedComponent>();
        _animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        DieEvent.OnAction += Die; 
    }
    private void OnDisable()
    {
        DieEvent.OnAction -= Die;
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
    private void Die()
    {
        ToRagdoll();
        _died = true;
    }
    private void Puke()
    {

    }
}
