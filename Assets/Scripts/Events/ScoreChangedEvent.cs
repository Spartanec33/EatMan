namespace UseEvents
{
    public static class ScoreChangedEvent
    {
        public static event ValueChanged OnAction;
        public static void ActivateEvent()
        {
            if (OnAction != null)
                OnAction();
        }
    }
}