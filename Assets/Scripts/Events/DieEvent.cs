public delegate void Die();
public static class DieEvent
{
    public static event Die OnAction;
    public static void ActivateEvent()
    {
        if (OnAction != null)
            OnAction();
    }
}