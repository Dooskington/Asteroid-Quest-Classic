using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatsComponent : MonoBehaviour
{
    public Text creditsText;

    private PlayerControllerComponent playerControllerComponent;

    private void Awake()
    {
        playerControllerComponent = FindObjectOfType<PlayerControllerComponent>();
    }

    private void Update()
    {
        creditsText.text = playerControllerComponent.credits.ToString();
    }
}
