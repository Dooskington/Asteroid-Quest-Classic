using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource stationMusic;
    public GameObject stationPanel;

    public void Update()
    {
        if (stationPanel.activeSelf)
        {
            backgroundMusic.volume = 0.0f;

            if (!stationMusic.isPlaying)
            {
                stationMusic.Play();
            }
        }
        else
        {
            backgroundMusic.volume = 0.5f;

            if (stationMusic.isPlaying)
            {
                stationMusic.Stop();
            }
        }
    }
}
