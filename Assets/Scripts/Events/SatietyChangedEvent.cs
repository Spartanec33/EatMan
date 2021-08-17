namespace UseEvents
{
    public delegate void ValueChanged();
    public static class SatietyChangedEvent
    {
        public static event ValueChanged OnAction;
        public static void ActivateEvent()
        {
            if (OnAction != null)
                OnAction();
        }
    }
}