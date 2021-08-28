namespace UseEvents
{
    public class OnReloadButtonClick
    {

        public static event ButtonClicked OnAction;
        public static void ActivateEvent()
        {
            if (OnAction != null)
                OnAction();
        }
    }
}