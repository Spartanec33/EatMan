using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixerBackGround;
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    private Resolution[] _resolutions;
    private void Awake()
    {
        InitializeResolutionDropdown();
    }

    private void InitializeResolutionDropdown()
    {
        _resolutions = Screen.resolutions;
        _resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + "x" + _resolutions[i].height;
            if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
            options.Add(option);
        }
        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int index)
    {
        Resolution resolution = _resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }
    public void SetQulity(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
    public void SetActiveMusic(bool state)
    {
        _audioMixerBackGround.audioMixer.SetFloat("backGroundVolume", state ? 0 : -80);
    }
}
