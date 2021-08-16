namespace UseEvents
{
    using UseFoodComponent.Personal;

    public delegate void ClickOnFood(Food food);
    public static class FoodClickEvent
    {
        public static event ClickOnFood OnAction;
        public static void ActivateEvent(Food food)
        {
            if (OnAction != null)
                OnAction(food);
        }
    }
}
