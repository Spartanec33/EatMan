using UnityEngine;
using UseEvents;
using UnityEngine.UI;
using UsePlayerComponents;

namespace UseUIComponents
{
    public class SatietyBar : MonoBehaviour
    {
        private Image _image;
        private HungerSystem _hungerSystem;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _hungerSystem = FindObjectOfType<HungerSystem>();
        }
        private void OnEnable()
        {
            SatietyChangedEvent.OnAction += ChangeSatietyBar;
        }
        private void OnDisable()
        {
            SatietyChangedEvent.OnAction += ChangeSatietyBar;
        }
        private void ChangeSatietyBar()
        {
            var value = _hungerSystem.Satiety / _hungerSystem.MaxSatiety;
            _image.fillAmount=value;
        }
    }
}