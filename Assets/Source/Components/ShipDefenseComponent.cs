using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipDefenseComponent : MonoBehaviour
{
    public float hull = 100.0f;
    public float maxHull = 100.0f;
    public float shield = 100.0f;
    public float maxShield = 100.0f;
    public float rechargeFrequency = 5.0f;
    public float rechargeAmount = 5.0f;
    public Slider hullSlider;
    public Slider shieldSlider;

    private float lastRechargeTime;

    public void Repair()
    {
        hull = maxHull;
    }

    private void Awake()
    {
        hullSlider.maxValue = maxHull;
        hullSlider.value = hull;

        shieldSlider.maxValue = maxShield;
        shieldSlider.value = shield;
    }

    private void Update()
    {
        if ((Time.time - lastRechargeTime) >= rechargeFrequency)
        {
            Recharge();
        }

        hull = Mathf.Clamp(hull, 0, maxHull);
        shield = Mathf.Clamp(shield, 0, maxShield);

        UpdateUI();
    }

    private void UpdateUI()
    {
        hullSlider.maxValue = maxHull;
        shieldSlider.maxValue = maxShield;

        hullSlider.value = Mathf.Lerp(hullSlider.value, hull, 2.5f * Time.deltaTime);
        shieldSlider.value = Mathf.Lerp(shieldSlider.value, shield, 2.5f * Time.deltaTime);
    }

    private void Recharge()
    {
        lastRechargeTime = Time.time;
        shield += rechargeAmount;
    }
}
