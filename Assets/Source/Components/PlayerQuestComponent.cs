using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestComponent : MonoBehaviour
{
    public UIQuestCompleteComponent questCompletePanel;
    public UINewQuestComponent newQuestPanel;
    public Vector3 startLocation;
    public StationControllerComponent destinationStation;

    private PlayerControllerComponent playerControllerComponent;

    public void CompleteQuest()
    {
        playerControllerComponent.score++;

        int reward = (int) (Mathf.Ceil(Vector3.Distance(startLocation, destinationStation.transform.position)) * 5);
        reward += (Random.Range(0, 10) * 5);

        playerControllerComponent.AddCredits(reward);

        questCompletePanel.Open(reward);
        Invoke("OpenNewQuestPanel", questCompletePanel.lengthTime + 0.5f);

        GenerateQuest();
    }

    public void GenerateQuest()
    {
        List<StationControllerComponent> stations = 
            new List<StationControllerComponent>(FindObjectsOfType<StationControllerComponent>());

        if (destinationStation)
        {
            stations.Remove(destinationStation);
        }

        int rand = Random.Range(0, stations.Count);
        destinationStation = stations[rand];
    }

    private void Awake()
    {
        playerControllerComponent = GetComponent<PlayerControllerComponent>();
        startLocation = transform.position;

        GenerateQuest();
    }

    private void OpenNewQuestPanel()
    {
        newQuestPanel.Open(this);
    }
}
