using System.Collections;
using UnityEngine;

namespace UseEvents
{
    public delegate void ButtonClicked();
    public class OnStartButtonClick
    {

        public static event ButtonClicked OnAction;
        public static void ActivateEvent()
        {
            if (OnAction != null)
                OnAction();
        }
    }
}