using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStationComponent : MonoBehaviour
{
    public Text title;
    public Button rechargeButton;
    public Button repairButton;
    public Button feedButton;
    public Button deliverButton;

    private PlayerControllerComponent player;
    private PlayerQuestComponent playerQuest;

    public void Open(StationControllerComponent stationControllerComponent)
    {
        title.text = stationControllerComponent.stationName;

        player = FindObjectOfType<PlayerControllerComponent>();

        playerQuest = player.GetComponent<PlayerQuestComponent>();
        if (playerQuest.destinationStation == stationControllerComponent)
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

    public void OnRechargeClick()
    {
        player.Recharge();
    }

    public void OnRepairClick()
    {
        player.Repair();
    }

    public void OnFeedClick()
    {
        player.Feed();
    }

    public void OnDeliverClick()
    {
        deliverButton.gameObject.SetActive(false);
        playerQuest.CompleteQuest();
    }
}
