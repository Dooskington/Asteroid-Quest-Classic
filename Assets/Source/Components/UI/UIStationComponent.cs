using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStationComponent : MonoBehaviour
{
    public Text title;
    public Button rechargeButton;
    public GameObject rechargeCostObject;
    public Text rechargeCostText;
    public Button repairButton;
    public GameObject repairCostObject;
    public Text repairCostText;
    public Button feedButton;
    public GameObject feedCostObject;
    public Text feedCostText;
    public Button deliverButton;

    public AudioEvent buySuccessAudio;
    public AudioEvent buyFailureAudio;
    public GameObject questPanel;
    public GameObject questContentPanel;

    public GameObject questButtonPrefab;

    private int rechargeCost;
    private int repairCost;
    private int feedCost;

    private StationControllerComponent station;
    private PlayerControllerComponent player;
    private ShipReactorComponent shipReactor;
    private ShipDefenseComponent shipDefense;
    private ShipCrewComponent shipCrew;
    private PlayerQuestComponent playerQuest;

    public void Open(StationControllerComponent stationControllerComponent)
    {
        station = stationControllerComponent;

        player = FindObjectOfType<PlayerControllerComponent>();
        playerQuest = player.GetComponent<PlayerQuestComponent>();
        shipReactor = player.GetComponent<ShipReactorComponent>();
        shipDefense = player.GetComponent<ShipDefenseComponent>();
        shipCrew = player.GetComponent<ShipCrewComponent>();

        ConstructUI();

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

        ConstructUI();
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

        ConstructUI();
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

        ConstructUI();
    }

    public void OnDeliverClick()
    {
        buySuccessAudio.Play();
        playerQuest.CompleteQuest();
        ConstructUI();
    }

    public void OnQuestClick(Quest quest)
    {
        buySuccessAudio.Play();
        playerQuest.BeginQuest(quest);
        station.GenerateQuests();
        ConstructUI();
    }

    private void ConstructUI()
    {
        title.text = station.StationName;

        rechargeCost = (int)Mathf.Ceil((shipReactor.maxCoreHealth - shipReactor.coreHealth) * 5);
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

        rechargeButton.enabled = (rechargeCost > 0);
        repairButton.enabled = (repairCost > 0);
        feedButton.enabled = (feedCost > 0);

        rechargeCostObject.SetActive(rechargeCost > 0);
        repairCostObject.SetActive(repairCost > 0);
        feedCostObject.SetActive(feedCost > 0);

        if (playerQuest.quest == null)
        {
            foreach (Transform child in questContentPanel.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (Quest quest in station.Quests)
            {
                GameObject buttonObject = Instantiate(questButtonPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                buttonObject.transform.SetParent(questContentPanel.transform, false);
                UIStationQuestButton questButton = buttonObject.GetComponent<UIStationQuestButton>();

                questButton.Setup(quest, this);
            }

            questPanel.SetActive(true);
            deliverButton.gameObject.SetActive(false);

            questPanel.GetComponent<ContentSizeFitter>().SetLayoutVertical();
            questContentPanel.GetComponent<ContentSizeFitter>().SetLayoutVertical();
            questContentPanel.GetComponent<VerticalLayoutGroup>().SetLayoutVertical();
        }
        else
        {
            questPanel.SetActive(false);

            if (playerQuest.quest.endStation == station)
            {
                deliverButton.gameObject.SetActive(true);
            }
            else
            {
                deliverButton.gameObject.SetActive(false);
            }
        }
    }

}
