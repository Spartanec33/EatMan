using UseEvents;
using UsePlayerComponents;
using UnityEngine;

namespace UseMove
{
    public class ScoreCounter : MonoBehaviour
    {
        private float _score;
        private SpeedComponent _speedCom;
        public float Score => _score;
        private void Start()
        {
            _speedCom = FindObjectOfType<SpeedComponent>();
        }
        private void FixedUpdate()
        {
            if (GameState.IsStarted && Player.IsDied==false)
            {
                _score += _speedCom.Speed * Time.deltaTime;
                OnScoreChanged.ActivateEvent();
            }
        }
    }
}