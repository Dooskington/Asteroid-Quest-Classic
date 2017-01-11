using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStationComponent : MonoBehaviour
{
    public Text title;
    public Button deliverButton;

    private PlayerQuestComponent playerQuestComponent;

    public void Open(StationControllerComponent stationControllerComponent)
    {
        title.text = stationControllerComponent.stationName;

        playerQuestComponent = FindObjectOfType<PlayerQuestComponent>();
        if (playerQuestComponent.destinationStation == stationControllerComponent)
        {
            deliverButton.gameObject.SetActive(true);
        }
        else
        {
            deliverButton.gameObject.SetActive(false);
        }

        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void OnDeliverClick()
    {
        deliverButton.gameObject.SetActive(false);
        playerQuestComponent.CompleteQuest();
    }
}
