using UseEvents;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UseFoodComponent.Logic;
using UseFoodComponent.Personal;

namespace UseUIComponents
{
    public class PropertyiesViewController : MonoBehaviour
    {
        [SerializeField] private Image _placePrefab;
        [SerializeField] private Image _baseImage;
        [SerializeField] private float _fixedBasePadding;
        [SerializeField] private float _minYAnchorProp = 0.1f;
        [SerializeField] private float _maxYAnchorProp = 0.9f;
        [SerializeField] private FoodPropertiesMap _foodPropertiesMap;

        private Dictionary<string, Sprite> _propertiesDict;
        private Image _thisPlace;
        private Image _thisProperty;

        private void OnEnable()
        {
            ChangeConstructionEvent.OnAction +=SetPropertiesOnUI;
        }
        private void OnDisable()
        {
            ChangeConstructionEvent.OnAction -=SetPropertiesOnUI;
        }
        private void Start()
        {
            _propertiesDict = _foodPropertiesMap.GetDictionary();
        }
        private void CreateUIElement(ref Image img,Image prefab, Transform parent)
        {
            img = Instantiate(prefab);
            img.transform.SetParent(parent);
            img.transform.localScale = Vector3.one;
            img.rectTransform.sizeDelta = Vector2.zero;
            img.rectTransform.anchoredPosition = Vector2.zero;
        }
        private void CreatePropertyiesPictures()
        {
            float fraction = 1f / FoodGetter.TargetProperties.Length;

            for (int i = 0; i < FoodGetter.TargetProperties.Length; i++)
            {
                CreateUIElement(ref _thisProperty, _baseImage, _thisPlace.transform);
                SetAnchors(fraction, i);
                _propertiesDict.TryGetValue(FoodGetter.TargetProperties[i], out Sprite sprite);
                _thisProperty.sprite = sprite;
            }

            void SetAnchors(float fraction, int i)
            {
                var anchorMin = _thisProperty.rectTransform.anchorMin;
                anchorMin.x = fraction * i;
                anchorMin.y = _minYAnchorProp;

                var anchorMax = _thisProperty.rectTransform.anchorMax;
                anchorMax.x = fraction * (i + 1);
                anchorMax.y = _maxYAnchorProp;

                _thisProperty.rectTransform.anchorMin = anchorMin;
                _thisProperty.rectTransform.anchorMax = anchorMax;
            }
        }

        private void SetPropertiesOnUI()
        {
            if (_thisPlace!=null)
            {
                Destroy(_thisPlace.gameObject);
            }
            CreateUIElement(ref _thisPlace, _placePrefab, transform);
            CreatePropertyiesPictures();
        }
    }
}