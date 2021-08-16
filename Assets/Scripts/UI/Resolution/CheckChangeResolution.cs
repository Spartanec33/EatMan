using UnityEngine;
using UseEvents;

namespace UseUIComponents.Resolution
{
    public class CheckChangeResolution : MonoBehaviour
    {
        private Canvas _canvas;
        private Vector2 _canvasOld;

        private void Start()
        {
            _canvas = GetComponent<Canvas>();
        }
        private void Update()
        {
            Vector2 canvasNow = new Vector2(_canvas.pixelRect.width, _canvas.pixelRect.height);
            if (_canvasOld != canvasNow)
            {
                ChangeResolutionEvent.ActivateEvent();
                _canvasOld = canvasNow;
            }
        }
    }
}
