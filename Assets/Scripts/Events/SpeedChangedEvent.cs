namespace UseEvents
{
    public static class SpeedChangedEvent
    {
        public static event ValueChanged OnAction;
        public static void ActivateEvent()
        {
            if (OnAction != null)
                OnAction();
        }
    }
}