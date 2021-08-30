using System.Collections;
using UnityEngine;
using UseMove;
using UseRoadComponent;

namespace UsePlayerComponents
{
    public class AfterFall : MonoBehaviour
    {
        [Header("Particle")]
        [SerializeField] private float _scaleParticlesOfThisPart;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private float _emissionByMaxSpeed;
        [SerializeField] private float _startSpeedByMaxSpeed;
        

        private SpeedComponent _speedCom;
        private AudioSource _audioSource;
        private void Start()
        {
            _speedCom = FindObjectOfType<SpeedComponent>();
            _audioSource = GetComponent<AudioSource>();
        }
        private void ChangeParticles(float speed)
        {
            var t = speed / (_speedCom.SpeedLimit/10) * _scaleParticlesOfThisPart;


            var emission = _particleSystem.emission;
            emission.rateOverTime = _emissionByMaxSpeed * t;

            var main = _particleSystem.main;
            main.startSpeed = _startSpeedByMaxSpeed * t;

        }
        private void ChangeSoundVolume(float speed)
        {
            var t = speed / (_speedCom.SpeedLimit / 10) * _scaleParticlesOfThisPart / 2 ;
            _audioSource.volume = Mathf.Lerp(0, 1, t);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<RoadPart>())
            {
                ChangeParticles(collision.relativeVelocity.magnitude);
                var particle = Instantiate<ParticleSystem>(_particleSystem, collision.contacts[0].point, _particleSystem.transform.rotation);
                ChangeSoundVolume(collision.relativeVelocity.magnitude);
                _audioSource.Play();

            }
        }
    }
}