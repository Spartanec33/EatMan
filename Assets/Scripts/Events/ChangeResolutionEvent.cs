public static class ChangeResolutionEvent
{
    public static event Change OnAction;
    public static void ActivateEvent()
    {
        if (OnAction != null)
            OnAction();
    }
}
