using UseEvents;
using UseFoodComponent.Logic;
using UnityEngine;

namespace UsePlayerComponents
{
    public class GameState : MonoBehaviour
    {

        public static bool IsStarted { get; private set; }

        private void OnEnable()
        {
            OnGameStarted.OnAction += ConfirmStart;
        }
        private void OnDisable()
        {
            OnGameStarted.OnAction -= ConfirmStart;
        }
        private void ConfirmStart()
        {
            IsStarted = true;
        }
    }
}