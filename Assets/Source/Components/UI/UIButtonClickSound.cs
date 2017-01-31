using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIButtonClickSound : MonoBehaviour
{
    public AudioEvent clickSound;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        if (!button || !clickSound)
        {
            Destroy(this);
        }

        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        clickSound.Play();
    }
}
