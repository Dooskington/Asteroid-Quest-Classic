using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableSoundComponent : MonoBehaviour
{
    public AudioEvent enableAudioEvent;
    public AudioEvent disableAudioEvent;

    private void OnEnable()
    {
        enableAudioEvent.Play();
    }

    private void OnDisable()
    {
        disableAudioEvent.Play();
    }
}
