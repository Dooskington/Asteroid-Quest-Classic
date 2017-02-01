using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFadeIn : MonoBehaviour
{
    public CanvasGroup fader;
    public float fadeInTime = 1.0f;
    public float fadeDelay = 1.0f;

    private void Awake()
    {
        fader.gameObject.SetActive(true);
        fader.alpha = 1.0f;

        Invoke("Fade", fadeDelay);
    }

    private void Fade()
    {
        Juice.Instance.FadeGroup(fader, fadeInTime, 0.0f);
    }
}
