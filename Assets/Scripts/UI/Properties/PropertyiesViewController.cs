using UseEvents;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UseFoodComponent.Logic;
using UseFoodComponent.Personal;
using System.Collections;

namespace UseUIComponents
{
    public class PropertyiesViewController : MonoBehaviour
    {
        [SerializeField] private Image _placePrefab;
        [SerializeField] private Image _baseImage;
        [SerializeField] private float _fixedBasePadding;
        [SerializeField] private float _minYAnchorProp = 0.1f;
        [SerializeField] private float _maxYAnchorProp = 0.9f;
        [SerializeField] private float _timeForResize;
        [SerializeField] private FoodPropertiesMap _foodPropertiesMap;

        private Dictionary<string, Sprite> _propertiesDict;
        private Image _thisPlace;
        private Image _thisProperty;
        private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

        private void OnEnable()
        {
            ChangeConstructionEvent.OnAction += ActivateSetProperties;
        }
        private void OnDisable()
        {
            ChangeConstructionEvent.OnAction -= ActivateSetProperties;
        }
        private void Start()
        {
            _propertiesDict = _foodPropertiesMap.GetDictionary();
        }
        private void CreateUIElement(ref Image img, Image prefab, Transform parent, Vector3 localScale)
        {
            img = Instantiate(prefab);
            img.transform.SetParent(parent);
            img.transform.localScale = localScale;
            img.rectTransform.sizeDelta = Vector2.zero;
            img.rectTransform.anchoredPosition = Vector2.zero;
        }
        private void CreatePropertyiesPictures()
        {
            float fraction = 1f / FoodGetter.TargetProperties.Length;

            for (int i = 0; i < FoodGetter.TargetProperties.Length; i++)
            {
                CreateUIElement(ref _thisProperty, _baseImage, _thisPlace.transform, Vector3.one) ;
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

        private IEnumerator SetPropertiesOnUI()
        {
            if (_thisPlace!=null)
            {
                yield return StartCoroutine(Resize(0));
                Destroy(_thisPlace.gameObject);
            }
            CreateUIElement(ref _thisPlace, _placePrefab, transform,Vector3.zero);
            CreatePropertyiesPictures();
            yield return StartCoroutine(Resize(1));
        }
        private IEnumerator Resize(float targetScale)
        {
            float allWay = Mathf.Abs(_thisPlace.transform.localScale.x-targetScale);
            float speed = allWay / _timeForResize;
            float coveredDistance = 0;
            float progress = 0;
            Vector3 scale = _thisPlace.transform.localScale;
            Vector3 finishScale = new Vector3(targetScale, targetScale, targetScale);
            while (progress<=1)
            {
                yield return _waitForFixedUpdate;
                progress = (coveredDistance / allWay);
   
                _thisPlace.transform.localScale = Vector3.Lerp(scale, finishScale, progress);
                coveredDistance += (speed * Time.deltaTime);
            }
            if (progress > 1)
                _thisPlace.transform.localScale = Vector3.Lerp(scale, finishScale, 1);
        }
        private void ActivateSetProperties()
        {
            StartCoroutine(SetPropertiesOnUI());
        }
    }
}