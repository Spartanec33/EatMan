public static class Stop2Event
{
    public static event Correct OnAction;
    public static void ActivateEvent()
    {
        if (OnAction != null)
            OnAction();
    }
}
