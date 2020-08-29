using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    Resolution[] resList;

    public Dropdown qualityDropdown;

    public Dropdown resolutionDropdown;

    public Slider volumeSlider;

    public Toggle fullscreenToggle;

    void Start()
    {
        resList = Screen.resolutions;
        int currentResIndex = 0;
        List<string> newList = new List<string>();
        resolutionDropdown.ClearOptions();
        for (int i = 0; i < resList.Length; i++)
        {
            string newObject = resList[i].width + "X" + resList[i].height;
            newList.Add(newObject);
            if (resList[i].width == 800 && resList[i].height == 600)
            {
                currentResIndex = i;
            }
        }
        resolutionDropdown.AddOptions(newList);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();
        volumeSlider.value = Settings.volumeValue;
        VolumeControl(volumeSlider.value);
        qualityDropdown.value = Settings.qualityIndex;
        QualityControl(qualityDropdown.value);
        fullscreenToggle.isOn = Settings.fullScreenStatus;
        FullScreenControl(fullscreenToggle.isOn);
        resolutionDropdown.value = Settings.resolutionIndex;
        ResolutionControl(resolutionDropdown.value);

    }

    public void VolumeControl(float value)
    {
        audioMixer.SetFloat("Volume", value * 20); //value from -2 to +1
        Settings.volumeValue = value;
    }

    public void QualityControl(int index)
    {
        QualitySettings.SetQualityLevel(index);
        Settings.qualityIndex = index;
    }

    public void FullScreenControl(bool status)
    {
        Screen.fullScreen = status;
        Settings.fullScreenStatus = status;
    }

    public void ResolutionControl(int index)
    {
        Resolution newRes = resList[index];
        Screen.SetResolution(newRes.width, newRes.height, Screen.fullScreen);
        Settings.resolutionIndex = index;
    }
}
