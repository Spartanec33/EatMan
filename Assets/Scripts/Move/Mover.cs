using UnityEngine;
using UseEvents;
using UseFoodComponent.Logic.OnClick;
using UseFoodComponent.NeedConstruction;
using UseRoadComponent;

namespace UseMove
{
    public class Mover : NeedConstruction
    {
        public static bool NeedOneTimeStop = true;
        private Road _road;
        private SpeedComponent _speedCom;

        private new void OnEnable()
        {
            base.OnEnable();
            OnCorrect.OnAction += CorrectPosition;
        }
        private new void OnDisable()
        {
            base.OnDisable();
            OnCorrect.OnAction -= CorrectPosition;
        }


        private void Start()
        {
            _speedCom = GetComponent<SpeedComponent>();
            _road = FindObjectOfType<Road>();
        }
        private void FixedUpdate()
        {
            if (!_speedCom.IsStop && !FoodOnClickFunctions.IsGoToFood)
            {
                Move(-_speedCom.Speed * Time.deltaTime);
            }
        }
        private void Move(float step)
        {
            if (_constraction != null)
                _constraction.transform.Translate(0, 0, step);

            if (_road != null)
                _road.transform.Translate(0, 0, step);

        }

        public void CorrectPosition(float delta)
        {
            Move(delta);
        }
    }
}
