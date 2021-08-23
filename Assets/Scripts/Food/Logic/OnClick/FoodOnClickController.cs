using System.Collections;
using UnityEngine;
using UseEvents;
using UseFoodComponent.Personal;
using UseMove;
using UsePlayerComponents;

namespace UseFoodComponent.Logic.OnClick
{
    [RequireComponent(typeof(FoodOnClickFunctions))]
    public class FoodOnClickController : UsingNewCoroutines
    {
        private FoodOnClickFunctions _func;

        private bool _isCoroutineActive;
        private static bool _isHaveTarget;
        private bool _canChangeTarget;
        private Food _oldFood;

        private SpeedComponent _speedCom;
        private Player _player;
        private HungerSystem _hungerSystem;
        private readonly WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

        public static bool IsHaveTarget => _isHaveTarget;

        private void Start()
        {
            _func = GetComponent<FoodOnClickFunctions>();
            _speedCom = FindObjectOfType<SpeedComponent>();
            _hungerSystem = FindObjectOfType<HungerSystem>();
            _player = FindObjectOfType<Player>();
        }
        private void OnEnable()
        {
            OnFoodClickEvent.OnAction += OnClick;
            OnDie.OnAction += StopAllCoroutinesOfThisClass;
        }
        private void OnDisable()
        {
            OnFoodClickEvent.OnAction -= OnClick;
            OnDie.OnAction -= StopAllCoroutinesOfThisClass;
        }

        public IEnumerator Final(Food food)
        {
            _func.CreateTargetParticle(food);
            _isHaveTarget = true;
            _oldFood = food;
            _isCoroutineActive = true;
            _canChangeTarget = true;
            Mover.NeedOneTimeStop = false;

            yield return StartCoroutineAndWaitIt(_func.ChangeTransform(food));


            //анимация поедания

            if (FoodComparer.Compare(food) != true)
            {
                food.Eat(false);
                _canChangeTarget = false;
                _isHaveTarget = false;
                StartCoroutineAndAddToList(_func.RotateToVomit());
                yield return StartCoroutineAndWaitIt(_player.Puke());
            }
            _canChangeTarget = true;

            while (FoodComparer.Compare(food) != true)
            {
                _speedCom.Stop();
                yield return _waitForFixedUpdate;
            }

            _canChangeTarget = false;

            food.Eat();
            OnDeleteConstruction.ActivateEvent();
            OnSpawnConstruction.ActivateEvent();
            _hungerSystem.AddSatiety();

            //действия по итогу сравнения
            _isCoroutineActive = false;
            _isHaveTarget = false;
            Mover.NeedOneTimeStop = true;

            yield return StartCoroutineAndWaitIt(_func.UndoTransform());
        }
        public void OnClick(Food food)
        {
            if (food != null && food != _oldFood)
            {
                if (_isCoroutineActive == false || _canChangeTarget)
                {
                    StopAllCoroutinesOfThisClass();
                    StartCoroutineAndAddToList(Final(food));
                }
            }
        }

        private new void StopAllCoroutinesOfThisClass()
        {
            _isCoroutineActive = false;
            _isHaveTarget = false;
            base.StopAllCoroutinesOfThisClass();
        }
    }
}