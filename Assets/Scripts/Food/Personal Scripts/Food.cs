using UnityEngine;
using UseEvents;
using UsePlayerComponents;

namespace UseFoodComponent.Personal
{
    [RequireComponent(typeof(Animator))]
    public class Food : ServiceElementsForFood
    {
        [SerializeField] private FoodPropertiesData _foodPropertiesData;
        [SerializeField] private ParticleSystem _eatParticle;

        private AudioSource _audioSource;
        public FoodPropertiesData FoodData => _foodPropertiesData;

        public void Init(RuntimeAnimatorController controller, AudioSource audioSource)
        {
            _audioSource = audioSource;
            _animator = GetComponent<Animator>();
            _animator.runtimeAnimatorController = controller;
            _animator.applyRootMotion = true;
            GetComponent<BoxCollider>().isTrigger = true;
        }

        public void Eat(bool withSound = true)
        {
            Instantiate(_eatParticle,transform.position,transform.rotation);
            if (withSound)
                _audioSource.Play();
            
            Destroy(gameObject);
        }

        private void OnMouseDown()
        {
            if (!Player.IsDied)
                OnFoodClickEvent.ActivateEvent(this);
        }
    }
}
