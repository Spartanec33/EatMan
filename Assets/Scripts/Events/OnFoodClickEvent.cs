using UseFoodComponent.Personal;
namespace UseEvents
{

    public delegate void ClickOnFood(Food food);
    public static class OnFoodClickEvent
    {
        public static event ClickOnFood OnAction;
        public static void ActivateEvent(Food food)
        {
            if (OnAction != null)
                OnAction(food);
        }
    }
}
