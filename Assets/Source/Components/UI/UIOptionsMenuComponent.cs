using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIOptionsMenuComponent : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public Toggle verticalSyncToggle;
    public Slider volumeSlider;
    public Button applyButton;

    private List<Resolution> resolutions = new List<Resolution>();
    private List<Dropdown.OptionData> resolutionOptions = new List<Dropdown.OptionData>();
    private Resolution resolution;
    private int screenWidth;
    private int screenHeight;

    private bool isVSyncOnDefault;
    private bool isVSyncOn;
    private bool isFullscreenDefault;
    private bool isFullscreen;
    private float volumeDefault;
    private float volume;

    public void Apply()
    {
        QualitySettings.vSyncCount = isVSyncOn ? 1 : 0;

        if (IsResolutionDifferent())
        {
            Screen.SetResolution(resolution.width, resolution.height, isFullscreen, resolution.refreshRate);
            screenWidth = resolution.width;
            screenHeight = resolution.height;
        }

        if (isFullscreen != isFullscreenDefault)
        {
            Screen.fullScreen = isFullscreen;
            isFullscreenDefault = isFullscreen;
        }

        PlayerPrefs.SetInt("VSyncCount", QualitySettings.vSyncCount);
        PlayerPrefs.Save();
        Setup();
    }

    private void Awake()
    {
        verticalSyncToggle.onValueChanged.AddListener((value) => { isVSyncOn = value; Refresh(); });
        fullscreenToggle.onValueChanged.AddListener((value) => { isFullscreen = value; Refresh(); });
        resolutionDropdown.onValueChanged.AddListener((value) => { resolution = resolutions[value]; Refresh(); });
        volumeSlider.onValueChanged.AddListener((value) => { volume = value; AudioListener.volume = value; PlayerPrefs.SetFloat("Volume", value); });

        screenWidth = Screen.width;
        screenHeight = Screen.height;
        isFullscreenDefault = Screen.fullScreen;

        Setup();
    }

    private void OnDisable()
    {
        Setup();
    }

    private void Refresh()
    {
        if ((isVSyncOn != isVSyncOnDefault) ||
            IsResolutionDifferent() ||
            (isFullscreen != isFullscreenDefault))
        {
            applyButton.interactable = true;
        }
        else
        {
            applyButton.interactable = false;
        }
    }

    private void Setup()
    {
        isVSyncOnDefault = (PlayerPrefs.GetInt("VSyncCount", QualitySettings.vSyncCount) == 1);
        volumeDefault = (PlayerPrefs.GetFloat("Volume", 1.0f));

        resolutionOptions.Clear();
        resolutionDropdown.ClearOptions();

        int currentValue = 0;
        foreach (Resolution res in Screen.resolutions.Distinct())
        {
            resolutions.Add(res);
            resolutionOptions.Add(new Dropdown.OptionData(res.width + "x" + res.height + " " + res.refreshRate + "hz"));

            if ((screenWidth == res.width) && 
                (screenHeight == res.height) && 
                (Screen.currentResolution.refreshRate == res.refreshRate))
            {
                currentValue = resolutionOptions.Count - 1;
            }
        }

        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentValue;

        resolution = resolutions[resolutionDropdown.value];
        isVSyncOn = isVSyncOnDefault;
        isFullscreen = isFullscreenDefault;
        volume = volumeDefault;

        fullscreenToggle.isOn = isFullscreen;
        verticalSyncToggle.isOn = isVSyncOn;
        volumeSlider.value = volume;

        Refresh();
    }

    private bool IsResolutionDifferent()
    {
        return (screenWidth != resolution.width) ||
               (screenHeight != resolution.height) ||
               (Screen.currentResolution.refreshRate != resolution.refreshRate);
    }
}
