using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestComponent : MonoBehaviour
{
    public StationControllerComponent destination;

    public void CompleteQuest()
    {
        Debug.Log("Quest completed.");
        GenerateQuest();
    }

    public void GenerateQuest()
    {
        List<StationControllerComponent> stations = 
            new List<StationControllerComponent>(FindObjectsOfType<StationControllerComponent>());

        if (destination)
        {
            stations.Remove(destination);
        }

        int rand = Random.Range(0, stations.Count);
        destination = stations[rand];
    }

    private void Awake()
    {
        GenerateQuest();
    }
}
