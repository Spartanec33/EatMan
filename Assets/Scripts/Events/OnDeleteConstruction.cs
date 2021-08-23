namespace UseEvents
{
    public static class OnDeleteConstruction
    {
        public static event ConstructionDelegate OnAction;
        public static void ActivateEvent()
        {
            if (OnAction != null)
                OnAction();
        }
    }
}
