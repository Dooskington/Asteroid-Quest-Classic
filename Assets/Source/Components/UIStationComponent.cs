using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStationComponent : MonoBehaviour
{
    public Text title;
    public Button rechargeButton;
    public Text rechargeCostText;
    public Button repairButton;
    public Text repairCostText;
    public Button feedButton;
    public Text feedCostText;
    public Button deliverButton;
    public AudioEvent buySuccessAudio;
    public AudioEvent buyFailureAudio;

    private int rechargeCost;
    private int repairCost;
    private int feedCost;

    private PlayerControllerComponent player;
    private ShipReactorComponent shipReactor;
    private ShipDefenseComponent shipDefense;
    private ShipCrewComponent shipCrew;
    private PlayerQuestComponent playerQuest;

    public void Open(StationControllerComponent stationControllerComponent)
    {
        player = FindObjectOfType<PlayerControllerComponent>();
        shipReactor = player.GetComponent<ShipReactorComponent>();
        shipDefense = player.GetComponent<ShipDefenseComponent>();
        shipCrew = player.GetComponent<ShipCrewComponent>();

        rechargeCost = (int) Mathf.Ceil((shipReactor.maxCoreHealth - shipReactor.coreHealth) * 5);
        repairCost = (int)Mathf.Ceil((shipDefense.maxHull - shipDefense.hull) * 5);
        feedCost = (shipCrew.maxHunger - shipCrew.hunger) * 5;

        rechargeButton.interactable = player.HasCredits(rechargeCost);
        repairButton.interactable = player.HasCredits(repairCost);
        feedButton.interactable = player.HasCredits(feedCost);

        rechargeCostText.text = rechargeCost.ToString();
        repairCostText.text = repairCost.ToString();
        feedCostText.text = feedCost.ToString();

        rechargeCostText.GetComponent<ContentSizeFitter>().SetLayoutHorizontal();
        repairCostText.GetComponent<ContentSizeFitter>().SetLayoutHorizontal();
        feedCostText.GetComponent<ContentSizeFitter>().SetLayoutHorizontal();

        if (rechargeCost <= 0)
        {
            rechargeButton.gameObject.SetActive(false);
        }
        else
        {
            rechargeButton.gameObject.SetActive(true);
        }

        if (repairCost <= 0)
        {
            repairButton.gameObject.SetActive(false);
        }
        else
        {
            repairButton.gameObject.SetActive(true);
        }

        if (feedCost <= 0)
        {
            feedButton.gameObject.SetActive(false);
        }
        else
        {
            feedButton.gameObject.SetActive(true);
        }

        title.text = stationControllerComponent.stationName;

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
        if (!player.TakeCredits(rechargeCost))
        {
            buyFailureAudio.Play();
            return;
        }

        buySuccessAudio.Play();
        player.Recharge();
    }

    public void OnRepairClick()
    {
        if (!player.TakeCredits(repairCost))
        {
            buyFailureAudio.Play();
            return;
        }

        buySuccessAudio.Play();
        shipDefense.Repair();
    }

    public void OnFeedClick()
    {
        if (!player.TakeCredits(feedCost))
        {
            buyFailureAudio.Play();
            return;
        }

        buySuccessAudio.Play();
        player.Feed();
    }

    public void OnDeliverClick()
    {
        deliverButton.gameObject.SetActive(false);
        playerQuest.CompleteQuest();
    }
}
