using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIQuestComponent : MonoBehaviour
{
    public Text questText;

    private PlayerQuestComponent playerQuestComponent;

    private void OnEnable()
    {
        playerQuestComponent = FindObjectOfType<PlayerQuestComponent>();
        if (playerQuestComponent)
        {
            questText.text = "Deliver the cargo to " + playerQuestComponent.destinationStation.stationName + ".";
        }
    }
}
