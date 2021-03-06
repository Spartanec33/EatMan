using System.Collections;
using UnityEngine;
using UseEvents;
using UseFoodComponent.Logic.OnClick;
using UsePlayerComponents;

namespace UseMove
{
    public class Stopper : MonoBehaviour
    {
        [SerializeField] private float _stopDistance = 10;

        private SpeedComponent _speedCom;
        private Distance _distance;

        private void Start()
        {
            _speedCom = GetComponent<SpeedComponent>();
            _distance = GetComponent<Distance>();
        }
        private void LateUpdate()
        {
            TryStop();
        }
        private void TryStop()
        {
            if (CheckForStop())
            {
                _speedCom.Stop();
            }
            else
                _speedCom.UnStop();
        }
        private bool CheckForStop()
        {
            var distance = _distance.Value;
            if(GameState.IsStarted == false)
            {
                return true;
            }
            else if ((distance <= _stopDistance) && Mover.NeedOneTimeStop)
            {
                OnCorrect.ActivateEvent(GetDelta(distance));
                return true;
            }
            else if (Player.IsDied || Player.IsPuke)
            {
                return true;
            }
            else if (FoodOnClickController.IsHaveTarget == false && distance <= _stopDistance)
            {
                return true;
            }
            return ((distance <= _stopDistance) && Mover.NeedOneTimeStop);
        }

        private float GetDelta(float distance)
        {
            return _stopDistance - distance;
        }

    }
}
