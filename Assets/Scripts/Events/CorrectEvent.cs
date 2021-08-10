public delegate void Correct(float delta);
public static class CorrectEvent
{
    public static event Correct OnAction;
    public static void ActivateEvent(float delta)
    {
        if (OnAction != null)
            OnAction(delta);
    }
}
