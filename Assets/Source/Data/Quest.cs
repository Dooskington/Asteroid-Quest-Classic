using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    private static int rewardCreditsPerMeter = 5;

    public StationControllerComponent startStation;
    public StationControllerComponent endStation;
    public int rewardCredits;
    public float distance;

    public Quest(StationControllerComponent startStation, StationControllerComponent endStation)
    {
        this.startStation = startStation;
        this.endStation = endStation;

        distance = Vector3.Distance(startStation.transform.position, endStation.transform.position);
        rewardCredits = (int) (distance * rewardCreditsPerMeter);
    }
}
