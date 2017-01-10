using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStationComponent : MonoBehaviour
{
    public Text title;
    public Button deliverButton;

    private PlayerQuestComponent playerQuestComponent;
    private StationControllerComponent stationControllerComponent;

    public void Open(StationControllerComponent stationControllerComponent)
    {
        this.stationControllerComponent = stationControllerComponent;

        title.text = stationControllerComponent.stationName;

        playerQuestComponent = FindObjectOfType<PlayerQuestComponent>();
        if (playerQuestComponent.destination == stationControllerComponent)
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
