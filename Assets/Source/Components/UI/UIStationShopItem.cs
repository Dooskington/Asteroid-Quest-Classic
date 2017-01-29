using Assets.Source.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStationShopItem : MonoBehaviour
{
    public Image itemIconImage;
    public Text itemNameText;
    public Text itemCostText;

    private Button button;
    private Upgrade upgrade;
    private PlayerControllerComponent player;

    public void Setup(Upgrade upgrade, PlayerControllerComponent player)
    {
        this.upgrade = upgrade;
        this.player = player;

        itemIconImage.sprite = upgrade.sprite;
        itemNameText.text = upgrade.upgradeName;
        itemCostText.text = upgrade.cost.ToString();
    }

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Update()
    {
        button.interactable = player.HasCredits(upgrade.cost);
    }
}
