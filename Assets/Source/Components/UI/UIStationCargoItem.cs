using Assets.Source.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStationCargoItem : MonoBehaviour
{
    public Image itemIconImage;
    public Text itemNameText;
    public Text itemCostText;

    public void Setup(Ore ore, int count, int cost, float costModifier)
    {
        itemIconImage.sprite = ore.sprite;
        itemNameText.text = ore.oreName + " [" + count + "]";

        int price = (int) ((cost * count) * costModifier);
        itemCostText.text = price.ToString();
    }
}
