using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Audio Event")]
public class AudioEvent : ScriptableObject
{
    public AudioClip[] audioClips;

    public RangedFloat volume;
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
        audioSourceComponent.volume = volume.GetRandomValue();
        audioSourceComponent.pitch = pitch.GetRandomValue();
        audioSourceComponent.Play();

        Destroy(gameObject, clip.length);
    }
}
