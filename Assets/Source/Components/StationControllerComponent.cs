using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationControllerComponent : MonoBehaviour
{
    private const int questCount = 3;

    public string stationName = "Station";
    public List<Quest> Quests { get; set; }

    private void Awake()
    {
        Quests = GenerateQuests(questCount);
    }

    private List<Quest> GenerateQuests(int count)
    {
        List<Quest> quests = new List<Quest>();

        List<StationControllerComponent> stations =
            new List<StationControllerComponent>(FindObjectsOfType<StationControllerComponent>());

        stations.Remove(this);

        for (int i = 0; i < count; i++)
        {
            StationControllerComponent endStation = stations[Random.Range(0, stations.Count)];
            stations.Remove(endStation);

            quests.Add(new Quest(this, endStation));
        }

        return quests;
    }
}
