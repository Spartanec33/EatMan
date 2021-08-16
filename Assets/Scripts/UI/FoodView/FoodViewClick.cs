using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UseEvents;
using UseFoodComponent.Personal;
using UsePlayerComponents;

namespace UseUIComponents.FoodView
{

    [RequireComponent(typeof(FoodViewMover))]
    public class FoodViewClick : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private RenderTexture _texture;
        [SerializeField] private Canvas _canvas;


        private RectTransform _rectTrans;
        private RawImage _rawImage;

        public Canvas Canvas => _canvas;
        public RectTransform RectTrans => _rectTrans;

        private void OnEnable()
        {
            ChangeResolutionEvent.OnAction += ChangeTextureResolution;
        }

        private void OnDisable()
        {
            ChangeResolutionEvent.OnAction -= ChangeTextureResolution;
        }

        private void Awake()
        {
            _rectTrans = GetComponent<RectTransform>();
            _rawImage = GetComponent<RawImage>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            var pressPosition = Convert(eventData.pressPosition);
            Ray ray = _camera.ScreenPointToRay(pressPosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.GetComponent<Food>())
                {
                    Food food = hit.collider.GetComponent<Food>();
                    if (!Player.IsDied)
                        FoodClickEvent.ActivateEvent(food);
                }
            }
        }
        private Vector2 Convert(Vector2 pressPosition)
        {
            var minAY = _rectTrans.anchorMin.y;
            var minAX = _rectTrans.anchorMin.x;
            var cHeight = _canvas.pixelRect.height;
            var cWidth = _canvas.pixelRect.width;
            return new Vector2(pressPosition.x - cWidth * minAX, pressPosition.y - cHeight * minAY);
        }
        private void ChangeTextureResolution()
        {
            _texture.Release();
            _texture.width = (int)(_rawImage.rectTransform.rect.width * _canvas.transform.localScale.x);
            _texture.height = (int)(_rawImage.rectTransform.rect.height * _canvas.transform.localScale.y);
        }
    }
}
