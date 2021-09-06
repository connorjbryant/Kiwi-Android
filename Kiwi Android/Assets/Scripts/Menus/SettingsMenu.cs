using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    Resolution[] resolutions;
    public GameObject ResolutionObject;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown qualityDropDown;

    void Start()
    {
        if (resolutionDropdown == null)
        {
            //return;
        }
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        print("Quality Level: " + PlayerPrefs.GetInt("CurrentQualityLevel"));

        qualityDropDown.value = PlayerPrefs.GetInt("CurrentQualityLevel") - 1;
    }
    public void setVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void setQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex + 1);
        PlayerPrefs.SetInt("CurrentQualityLevel", qualityIndex + 1);
    }

    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void setResolution(int resolutionIndex)
    {
        if (Screen.fullScreen)
        {
            return;
        }
        Debug.Log("Should change resolution");
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
