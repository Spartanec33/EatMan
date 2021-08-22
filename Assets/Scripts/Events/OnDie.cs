namespace UseEvents
{
    public delegate void Die();
    public static class OnDie
    {
        public static event Die OnAction;
        public static void ActivateEvent()
        {
            if (OnAction != null)
                OnAction();
        }
    }
}