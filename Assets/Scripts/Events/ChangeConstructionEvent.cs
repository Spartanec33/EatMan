public delegate void Change();
public class ChangeConstructionEvent
{
    public event Change Action;
    public void ActivateEvent()
    {
        if (Action != null)
            Action();
    }
}
