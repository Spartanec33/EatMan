using UnityEngine;
using UseEvents;
using UsePlayerComponents;

namespace UseFoodComponent.Personal
{
    [RequireComponent(typeof(PersonalFoodAnimation))]
    public class Food : ServiceElementsForFood
    {
        [SerializeField] private FoodPropertiesData _foodPropertiesData;
        [SerializeField] private ParticleSystem _eatParticle;

        private AudioSource _audioSource;
        public FoodPropertiesData FoodData => _foodPropertiesData;

        public void Init(FoodAnimData data, AudioSource audioSource)
        {
            _audioSource = audioSource;

            var animData = GetComponent<PersonalFoodAnimation>();
            animData.Init(data);
            
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
