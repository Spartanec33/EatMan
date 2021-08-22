namespace UseEvents
{
    public delegate void OnPlayerClicked();
    public static class OnPlayerClick
    {
        public static event OnPlayerClicked OnAction;
        public static void ActivateEvent()
        {
            if (OnAction != null)
                OnAction();
        }
    }
}