using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestComponent : MonoBehaviour
{
    public UIQuestCompleteComponent questCompletePanel;
    public UINewQuestComponent newQuestPanel;
    public Quest quest;

    private PlayerControllerComponent player;

    public void BeginQuest(Quest quest)
    {
        this.quest = quest;
        newQuestPanel.Open(quest);
    }

    public void CompleteQuest()
    {
        player.score++;
        player.AddCredits(quest.rewardCredits);

        questCompletePanel.Open(quest.rewardCredits);

        quest = null;
    }

    private void Awake()
    {
        player = GetComponent<PlayerControllerComponent>();
    }
}
