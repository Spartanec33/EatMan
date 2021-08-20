namespace UseEvents
{
    public static class ChangeResolutionEvent
    {
        public static event ValueChanged OnAction;
        public static void ActivateEvent()
        {
            if (OnAction != null)
                OnAction();
        }
    }

}
