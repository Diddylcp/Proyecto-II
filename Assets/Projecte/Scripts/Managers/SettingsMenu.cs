using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    Resolution[] allResolutions;
    [SerializeField] TMPro.TMP_Dropdown resolutionDropdown;

    private void Start()
    {
        allResolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i=0; i<allResolutions.Length; i++)
        {
            string option = allResolutions[i].width + " X " + allResolutions[i].height;
            options.Add(option);
            if (allResolutions[i].width == Screen.currentResolution.width && allResolutions[i].height == Screen.currentResolution.height) currentResolutionIndex = i;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetVolume (float _volume)
    {
        audioMixer.SetFloat("MainVolume", _volume);
    }

    public void SetFullScreen(bool _fullScreen)
    {
        Screen.fullScreen = _fullScreen;
    }

    public void SetResolution(int _resolutionIndex)
    {
        Resolution resolution = allResolutions[_resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
