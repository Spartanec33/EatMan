using UseEvents;
using UnityEngine;
using UseFoodComponent.Logic;

namespace UseUIComponents
{
    class MainMenu: MonoBehaviour
    {
        private bool WasClicked;
        public void StartButtonClick()
        {
            if (WasClicked == false)
            {
                OnStartButtonClick.ActivateEvent();
                OnSpawnConstruction.ActivateEvent();
                WasClicked = true;
            }
        }
        public void QuitButtonClick()
        {
            Application.Quit();
        }
    }
}
