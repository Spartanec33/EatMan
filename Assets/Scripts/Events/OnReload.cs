using UnityEngine;

namespace UseEvents
{
    public class OnReload : MonoBehaviour
    {

        public static event ButtonClicked OnAction;
        public static void ActivateEvent()
        {
            if (OnAction != null)
                OnAction();
        }
    }
}