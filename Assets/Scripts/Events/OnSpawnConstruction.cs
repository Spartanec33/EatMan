namespace UseEvents
{
    public delegate void ConstructionDelegate();
    public static class OnSpawnConstruction
    {
        public static event ConstructionDelegate OnAction;
        public static void ActivateEvent()
        {
            if (OnAction != null)
                OnAction();
        }
    }
}
