using UseEvents;
using UnityEngine;


namespace UsePlayerComponents
{
    public class ClicableZone : MonoBehaviour
    {
        private void OnMouseDown()
        {
            OnPlayerClick.ActivateEvent();
        }
    }
}
