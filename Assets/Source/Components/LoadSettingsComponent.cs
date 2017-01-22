using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSettingsComponent : MonoBehaviour
{
    private void Awake()
    {
        LoadQualitySettings();
    }

    private void LoadQualitySettings()
    {
        QualitySettings.vSyncCount = PlayerPrefs.GetInt("VSyncCount", QualitySettings.vSyncCount);
        AudioListener.volume = PlayerPrefs.GetFloat("Volume", 1.0f);
    }
}
