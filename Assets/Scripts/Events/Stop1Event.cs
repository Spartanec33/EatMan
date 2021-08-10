public delegate void Correct();
public static class Stop1Event
{
    public static event Correct OnAction;
    public static void ActivateEvent()
    {
        if (OnAction != null)
            OnAction();
    }
}
