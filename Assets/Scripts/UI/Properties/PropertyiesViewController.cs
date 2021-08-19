using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UseUIComponents
{
    public class PropertyiesViewController : MonoBehaviour
    {
        [SerializeField] private Image _placePrefab;
        [SerializeField] private Image _baseImage;
        [SerializeField] private float _fixedBasePadding;
        [SerializeField] private float _minYAnchorProp = 0.1f;
        [SerializeField] private float _maxYAnchorProp = 0.9f;

        private Image _thisPlace;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                CreatePlace();
            }
        }
        private void CreatePlace()
        {
            _thisPlace = Instantiate(_placePrefab);
            _thisPlace.transform.SetParent(transform);
            _thisPlace.rectTransform.sizeDelta = new Vector2(0, 0);
            _thisPlace.rectTransform.anchoredPosition = new Vector2(0, 0);
        }
        private void CreatePropertyiesPictures(int amount)
        {

            for (int i = 0; i < amount; i++)
            {

            }
        }
    }
}