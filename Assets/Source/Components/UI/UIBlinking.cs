using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBlinking : MonoBehaviour
{
    public float blinkFrequency = 1.0f;

    private float lastBlinkTime;
    private CanvasGroup CanvasGroup { get; set; }

    private void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if ((Time.time - lastBlinkTime) >= blinkFrequency)
        {
            Blink();
        }
    }

    private void Blink()
    {
        lastBlinkTime = Time.time;
        CanvasGroup.alpha = (CanvasGroup.alpha == 1) ? 0 : 1;
    }
}
