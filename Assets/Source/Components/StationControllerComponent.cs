using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationControllerComponent : MonoBehaviour
{
    private const int questCount = 3;

    public string StationName { get; set; }
    public List<Quest> Quests { get; set; }

    private void Awake()
    {
        StationDataComponent stationData = FindObjectOfType<StationDataComponent>();
        StationName = stationData.TakeName();

        GetComponent<MapBlipComponent>().blipName = StationName;

        GenerateQuests();
    }

    public void GenerateQuests()
    {
        Quests = new List<Quest>();

        List<StationControllerComponent> stations =
            new List<StationControllerComponent>(FindObjectsOfType<StationControllerComponent>());

        stations.Remove(this);

        for (int i = 0; i < questCount; i++)
        {
            StationControllerComponent endStation = stations[Random.Range(0, stations.Count)];
            stations.Remove(endStation);

            Quests.Add(new Quest(this, endStation));
        }
    }
}
