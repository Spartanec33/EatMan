using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TMPro;
using UseEvents;
using UseMove;

namespace UseUIComponents
{
    class Statistic: MonoBehaviour
    {
        [SerializeField] private TMP_Text _deathCountText;
        [SerializeField] private TMP_Text _trueAndFalseCountText;
        [SerializeField] private TMP_Text _trueAndFalsePercentText;
        [SerializeField] private TMP_Text _highScoreText;
        [SerializeField] private TMP_Text _maxSpeedText;

        private int _deathCount;
        private int _falseCount;
        private int _trueCount;
        private int _highScore;
        private int _maxSpeed;
        private SpeedComponent _speedCom;
        private ScoreCounter _scoreCounter;

        private void OnEnable()
        {
            OnDie.OnAction += DeathCountUp;
            OnDie.OnAction += CheckChangeMaxSpeedAndHighscore;
            OnTrueFoodEat.OnAction += TrueCountUp;
            OnFalseFoodEat.OnAction += FalseCountUp;
            OnDie.OnAction += Save;
        }
        private void OnDisable()
        {
            OnDie.OnAction -= DeathCountUp;
            OnDie.OnAction -= CheckChangeMaxSpeedAndHighscore;
            OnTrueFoodEat.OnAction -= TrueCountUp;
            OnFalseFoodEat.OnAction -= FalseCountUp;
            OnDie.OnAction -= Save;
        }
        private void Start()
        {
            _speedCom = FindObjectOfType<SpeedComponent>();
            _scoreCounter = FindObjectOfType<ScoreCounter>();
            Load();
            SetValueToStats();
        }

        private void Save()
        {
            var bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/Statistics.dat");
            SaveData data = new SaveData
            {
                _deathCount = _deathCount,
                _trueCount = _trueCount,
                _falseCount = _falseCount,
                _highScore = _highScore,
                _maxSpeed = _maxSpeed,
            };
            bf.Serialize(file, data);
            file.Close();
        }
        private void Load()
        {
            if (File.Exists(Application.persistentDataPath+ "/Statistics.dat"))
            {
                var bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/Statistics.dat", FileMode.Open);
                SaveData data = (SaveData)bf.Deserialize(file);
                file.Close();
                _deathCount = data._deathCount;
                _trueCount = data._trueCount;
                _falseCount = data._falseCount;
                _highScore = data._highScore;
                _maxSpeed = data._maxSpeed;
            }
        }
        private void SetValueToStats()
        {
            _deathCountText.text = _deathCount.ToString();
            _trueAndFalseCountText.text = $"{_trueCount}/{_falseCount}";
            if (_falseCount + _trueCount == 0)
                _trueAndFalsePercentText.text = "100%";
            else
                _trueAndFalsePercentText.text = $"{(int)((float)_trueCount / (_falseCount + _trueCount) * 100)}%";
            _highScoreText.text = $"HighScore:{_highScore}";
            _maxSpeedText.text = $"MaxSpeed:{_maxSpeed}";

        }
        private void TrueCountUp() => _trueCount += 1;
        private void FalseCountUp() => _falseCount += 1;
        private void DeathCountUp() => _deathCount += 1;
        private void CheckChangeMaxSpeedAndHighscore()
        {
            if (_speedCom.MaxSpeed > _maxSpeed)
                _maxSpeed = _speedCom.MaxSpeed;
            if (_scoreCounter.Score > _highScore)
                _highScore = (int)_scoreCounter.Score;
        }
    }

    [Serializable]
    class SaveData
    {
        public int _deathCount;
        public int _trueCount;
        public int _falseCount;
        public int _highScore;
        public int _maxSpeed;
    }

}
