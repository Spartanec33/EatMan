using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(HungerSystem))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _rigidbodies;
    [SerializeField] private ParticleSystem _pukeParticle;

    private static bool _isDied;
    private static bool _isPuke;
    private SpeedComponent _speedCom;
    private Animator _animator;
    private readonly WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

    public static bool IsDied => _isDied;
    public static bool IsPuke => _isPuke;
    public static bool IsGoToFood { get; set; }

    private void Start()
    {
        _speedCom = FindObjectOfType<SpeedComponent>();
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
            Die();
        }
    }
    private void ToRagdoll()
    {
        _animator.enabled = false;
        foreach (var item in _rigidbodies)
        {
            item.isKinematic = false;
            item.velocity = new Vector3(0, 0, _speedCom.Speed / 10);
        }
        _speedCom.Stop();
    }
    private void Die()
    {
        ToRagdoll();
        _isDied = true;
    }
    public IEnumerator Puke()
    {
        _isPuke = true;
        _pukeParticle.Play();
        while(_pukeParticle.isPlaying)
        {
            _speedCom.Stop();
            yield return _waitForFixedUpdate;
        }
        _isPuke = false;
    }
}
