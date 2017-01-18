using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStationQuestButton : MonoBehaviour
{
    public Text endStationName;
    public Text rewardCredits;
    public Image safety;
    public Color easyColor;
    public Color normalColor;
    public Color hardColor;

    private Quest quest;
    private UIStationComponent stationUI;

    public void Setup(Quest quest, UIStationComponent stationUI)
    {
        this.quest = quest;
        this.stationUI = stationUI;

        endStationName.text = quest.endStation.stationName;
        rewardCredits.text = quest.rewardCredits.ToString();

        if (quest.distance >= 100)
        {
            safety.color = hardColor;
        }
        else if (quest.distance >= 50)
        {
            safety.color = normalColor;
        }
        else
        {
            safety.color = easyColor;
        }
    }

    public void OnClick()
    {
        stationUI.OnQuestClick(quest);
    }
}
