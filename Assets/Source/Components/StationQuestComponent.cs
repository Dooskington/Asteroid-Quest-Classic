using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationQuestComponent : MonoBehaviour
{
    private PlayerQuestComponent playerQuestComponent;

    private void Awake()
    {
        playerQuestComponent = FindObjectOfType<PlayerQuestComponent>();
    }
}
