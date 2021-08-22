namespace UseEvents
{
    public static class OnScoreChanged
    {
        public static event ValueChanged OnAction;
        public static void ActivateEvent()
        {
            if (OnAction != null)
                OnAction();
        }
    }
}