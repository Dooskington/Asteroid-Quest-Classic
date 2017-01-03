using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Audio Event")]
public class AudioEvent : ScriptableObject
{
    public AudioClip[] audioClips;

    public bool isVolumeRandom = true;
    public RangedFloat volume;

    public bool isPitchRandom = true;
    public RangedFloat pitch;
	
    public void Play(Vector3 position)
    {
        if (audioClips.Length == 0)
        {
            return;
        }

        AudioClip clip = audioClips[Random.Range(0, audioClips.Length)];

        GameObject gameObject = new GameObject("AudioEvent_" + clip.name);
        AudioSource audioSourceComponent = gameObject.AddComponent<AudioSource>();

        audioSourceComponent.clip = clip;

        if (isVolumeRandom)
        {
            audioSourceComponent.volume = volume.GetRandomValue();
        }

        if (isPitchRandom)
        {
            audioSourceComponent.pitch = pitch.GetRandomValue();
        }

        audioSourceComponent.Play();

        Destroy(gameObject, clip.length);
    }

    public void Play()
    {
        Play(Vector3.zero);
    }
}
