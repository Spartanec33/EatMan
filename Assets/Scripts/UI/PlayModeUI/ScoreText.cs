using UnityEngine;
using TMPro;
using UseEvents;
using UseMove;

namespace UseUIComponents
{
    public class ScoreText : MonoBehaviour
    {
        private TMP_Text _text;
        private ScoreCounter _scoreCounter;

        private void Start()
        {
            _text = GetComponent<TMP_Text>();
            _scoreCounter=FindObjectOfType<ScoreCounter>();
        }
        private void OnEnable()
        {
            OnScoreChanged.OnAction += ChangeText;
        }
        private void OnDisable()
        {
            OnScoreChanged.OnAction += ChangeText;
        }
        private void ChangeText()
        {
            var value = (int)_scoreCounter.Score ;
            _text.text = $"Score: {value}";
        }
    }
}
