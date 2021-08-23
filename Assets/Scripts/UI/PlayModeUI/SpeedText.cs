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
            OnSpeedChanged.OnAction += ChangeText;
        }
        private void OnDisable()
        {
            OnSpeedChanged.OnAction -= ChangeText;
        }
        private void ChangeText()
        {
            var value = (int)_speedCom.Speed;
            _text.text=$"Speed: {value}";
        }
    }
}
