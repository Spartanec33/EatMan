using System.Collections;
using UnityEngine;

namespace UseEvents
{
    public delegate void GameStarted();
    public static class OnGameStarted
    {
        public static event GameStarted OnAction;
        public static void ActivateEvent()
        {
            if (OnAction != null)
                OnAction();
            Debug.Log("OngameStarted");

        }
    }
}