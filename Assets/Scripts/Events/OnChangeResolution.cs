namespace UseEvents
{
    public static class OnChangeResolution
    {
        public static event ValueChanged OnAction;
        public static void ActivateEvent()
        {
            if (OnAction != null)
                OnAction();
        }
    }

}
