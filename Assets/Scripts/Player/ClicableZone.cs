using UseEvents;
using UnityEngine;


namespace UsePlayerComponents
{
    public class ClicableZone : MonoBehaviour
    {
        private void OnMouseDown()
        {
            OnPlayerClickedEvent.ActivateEvent();
        }
    }
}
