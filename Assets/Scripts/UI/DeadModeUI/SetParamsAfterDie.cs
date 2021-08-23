using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UseEvents;
using UseMove;

public class SetParamsAfterDie : MonoBehaviour
{
    [SerializeField] private TMP_Text _maxSpeedText;
    [SerializeField] private TMP_Text _scoreText;

    private SpeedComponent _speedCom;
    private ScoreCounter _scoreCounter;
    private void OnEnable()
    {
        OnDie.OnAction += SetText;
    }
    private void OnDisable()
    {
        OnDie.OnAction -= SetText;
    }
    private void Start()
    {
        _speedCom = FindObjectOfType<SpeedComponent>();
        _scoreCounter = FindObjectOfType<ScoreCounter>();
    }
    private void SetText()
    {
        _maxSpeedText.text = $"Max Speed: {_speedCom.MaxSpeed:0}";
        var scoreValue = (int)_scoreCounter.Score;
        _scoreText.text = $"Score: {scoreValue}";
    }
}
