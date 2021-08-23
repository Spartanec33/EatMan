using System.Collections;
using UnityEngine;
using UseEvents;
using UseMove;

namespace UsePlayerComponents
{
    [RequireComponent(typeof(PlayerAnimation))]
    [RequireComponent(typeof(HungerSystem))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpeedAdapt))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private Rigidbody[] _rigidbodies;
        [SerializeField] private GameObject[] _touchZones;
        [SerializeField] private ParticleSystem _pukeParticle;
        [SerializeField] private AudioSource _pukeSound;

        private static bool _isDied;
        private static bool _isPuke;
        private SpeedComponent _speedCom;
        private Animator _animator;
        private readonly WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

        public static bool IsDied => _isDied;
        public static bool IsPuke => _isPuke;

        private void Start()
        {
            _isDied = false;
            _isPuke = false;
            _speedCom = FindObjectOfType<SpeedComponent>();
            _animator = GetComponent<Animator>();
        }
        private void OnEnable()
        {
            OnDie.OnAction += Die;
        }

        private void OnDisable()
        {
            OnDie.OnAction -= Die;
        }

        private void ToRagdoll()
        {
            _animator.enabled = false;
            foreach (var item in _rigidbodies)
            {
                item.isKinematic = false;
                item.velocity = new Vector3(0, 0, _speedCom.Speed / 10);
            }
            foreach (var item in _touchZones)
            {
                item.SetActive(false);
            }
        }
        private void Die()
        {
            ToRagdoll();
            _isDied = true;
        }
        public IEnumerator Puke()
        {
            _isPuke = true;
            _pukeSound.Play();
            _pukeParticle.Play();
            while (_pukeParticle.isPlaying)
                yield return _waitForFixedUpdate;
            
            _isPuke = false;
        }
    }
}
