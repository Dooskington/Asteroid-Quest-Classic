using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    public bool isOpen = false;
    public float fadeTime = 1.0f;

    public void Open()
    {
        gameObject.SetActive(true);
        isOpen = true;
        Juice.Instance.FadeGroup(GetComponent<CanvasGroup>(), fadeTime, 1.0f);
    }

    public void Close()
    {
        isOpen = false;
        Juice.Instance.FadeGroup(GetComponent<CanvasGroup>(), fadeTime, 0.0f, true, () => { gameObject.SetActive(false); });
    }
}
