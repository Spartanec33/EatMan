public delegate void Change();
public static class ChangeConstructionEvent
{
    public static event Change OnAction;
    public static void ActivateEvent()
    {
        if (OnAction != null)
            OnAction();
    }
}
