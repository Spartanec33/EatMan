using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    [Header("BackgroundMusic")]
    [SerializeField] private AudioMixerGroup _audioMixerBackGround;
    [SerializeField] private Toggle _backgroundMusicToggle;

    [Header("Resolution")] 
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    [SerializeField] private int _resolutionsLessThanTheCurrent;
    [SerializeField] private float _resolutionMultiplier = 0.9f;

    [Header("Graphic")]
    [SerializeField] private TMP_Dropdown _graphicDropdown;

    private List<Resolution> _resolutions = new List<Resolution>();
    private int _currentResolutionIndex;
    private Resolution _mainResolution;


    private void Awake()
    {
        if (!PlayerPrefs.HasKey("screenWidth"))
            SaveBaseResolution();
        _mainResolution = GetBaseResolution();
        InitializeResolutionDropdown();
    }
    private void Start()
    {
        LoadData();
    }

    private void InitializeResolutionDropdown()
    {
        Resolution baseResolution = _mainResolution;

        baseResolution = _mainResolution;
        _resolutions.Add(baseResolution);

        for (int i = 0; i < _resolutionsLessThanTheCurrent; i++)
        {
            baseResolution = GetResolution(baseResolution);
        }

        _resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        for (int i = 0; i < _resolutions.Count; i++)
        {
            string option = _resolutions[i].ToString();
            if (_resolutions[i].width == _mainResolution.width && _resolutions[i].height == _mainResolution.height)
                _currentResolutionIndex = i;
            options.Add(option);
        }
        _resolutionDropdown.AddOptions(options);

        Resolution GetResolution(Resolution baseResolution)
        {
            baseResolution.width = (int)(baseResolution.width * _resolutionMultiplier);
            baseResolution.height = (int)(baseResolution.height * _resolutionMultiplier);
            var resToAdd = new Resolution
            {
                width = baseResolution.width,
                height = baseResolution.height,
                refreshRate = baseResolution.refreshRate
            };
            _resolutions.Add(resToAdd);
            return baseResolution;
        }
    }

    public void SetResolution(int index)
    {
        Resolution resolution = _resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, true);
        PlayerPrefs.SetInt("resolutionIndex", index);
        PlayerPrefs.Save();
    }
    public void SetQulity(int index)
    {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt("graphicIndex", index);
        PlayerPrefs.Save();
    }
    public void ToggleBackgroundMusic(bool state)
    {
        SetBackgroundMusic(state);
        PlayerPrefs.SetInt("isBackgroundMusicActive", state ? 1 : 0);
        PlayerPrefs.Save();
    }
    private void SetBackgroundMusic(bool state)
    {
        _audioMixerBackGround.audioMixer.SetFloat("backGroundVolume", state ? 0 : -80);
    }

    private void LoadData()
    {
        _resolutionDropdown.value = PlayerPrefs.GetInt("resolutionIndex", _currentResolutionIndex);
        _resolutionDropdown.RefreshShownValue();

        _graphicDropdown.value = PlayerPrefs.GetInt("graphicIndex", 3);
        _graphicDropdown.RefreshShownValue();

        _backgroundMusicToggle.isOn = PlayerPrefs.GetInt("isBackgroundMusicActive", 1) == 1 ? true : false;
        SetBackgroundMusic(_backgroundMusicToggle.isOn);
    }

    private void SaveBaseResolution()
    {
        var baseResolution = Screen.currentResolution;
        PlayerPrefs.SetInt("screenWidth", baseResolution.width);
        PlayerPrefs.SetInt("screenHeight", baseResolution.height);
    }
    private Resolution GetBaseResolution()
    {
        var resolution = new Resolution
        {
            width = PlayerPrefs.GetInt("screenWidth"),
            height = PlayerPrefs.GetInt("screenHeight"),
        };
        return resolution;
    }
}
