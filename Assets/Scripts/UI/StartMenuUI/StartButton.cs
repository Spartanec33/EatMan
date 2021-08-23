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
                OnSpawnConstruction.ActivateEvent();
                WasClicked = true;
            }
        }
    }
}
