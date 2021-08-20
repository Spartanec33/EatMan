using System.Collections;
namespace UseEvents 
{ 
    public static class ChangeConstructionEvent
    {
        public static event ValueChanged OnAction;
        public static void ActivateEvent()
        {
            if (OnAction != null)
                OnAction();
        }
    }
}
