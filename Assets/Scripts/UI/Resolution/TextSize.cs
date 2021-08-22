using System.Collections;
using UnityEngine;
using TMPro;
using UseEvents;

namespace UseUIComponents
{
    public class TextSize : MonoBehaviour
    {
        private TMP_Text _text;

        private void OnEnable()
        {
            OnChangeResolution.OnAction += Zjopa;
        }
        private void OnDisable()
        {
            OnChangeResolution.OnAction -= Zjopa;
        }

        private void Start()
        {
            _text = GetComponent<TMP_Text>();
        }
        private void Update()
        {
            Zjopa();
        }
        private void Zjopa()
        {

            //_text.autoSizeTextContainer = true;
        }
    }
}