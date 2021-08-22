using UseEvents;
using UnityEngine;
using UseFoodComponent.Logic;

namespace UseUIComponents
{
    class StartButton: MonoBehaviour
    {
        private bool WasClicked;
        public void OnClick()
        {
            if (WasClicked == false)
            {
                OnStartButtonClick.ActivateEvent();
                FoodSpawner.Spawn();
                WasClicked = true;
            }
        }
    }
}
