using UnityEngine;
using TMPro;
using UseEvents;
using UseMove;

namespace UseUIComponents
{
    public class SpeedText : MonoBehaviour
    {
        private TMP_Text _text;
        private SpeedComponent _speedCom;
        
        private void Start()
        {
            _speedCom = FindObjectOfType<SpeedComponent>();
            _text = GetComponent<TMP_Text>();
        }
        private void OnEnable()
        {
            SpeedChangedEvent.OnAction += ChangeText;
        }
        private void OnDisable()
        {
            SpeedChangedEvent.OnAction += ChangeText;
        }
        private void ChangeText()
        {
            var value = (int)_speedCom.Speed;
            _text.text=$"Speed: {value}";
        }
    }
}
