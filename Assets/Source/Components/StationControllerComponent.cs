using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationControllerComponent : MonoBehaviour
{
    public string StationName { get; set; }

    private void Awake()
    {
        StationDataComponent stationData = FindObjectOfType<StationDataComponent>();
        StationName = stationData.TakeName();

        GetComponent<MapBlipComponent>().blipName = StationName;
    }
}
