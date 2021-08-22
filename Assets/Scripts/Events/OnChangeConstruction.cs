using System.Collections;
using UnityEngine;
namespace UseEvents 
{ 
    public static class OnChangeConstruction
    {
        public static event ValueChanged OnAction;
        public static void ActivateEvent()
        {
            if (OnAction != null)
                OnAction();
        }
    }
}
