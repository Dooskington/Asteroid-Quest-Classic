using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipReactorComponent : MonoBehaviour
{
    public float coreHealth = 100.0f;
    public float maxCoreHealth = 100.0f;

    public float coreUsage = 0.0f;
    public float maxCoreUsage = 100.0f;
    public float usageModifier = 1.0f;

    public Slider coreHealthSlider;

    public void UsePower(float amount)
    {
        coreUsage += amount;
    }

    private void Update()
    {
        coreHealth -= (coreUsage / 1000.0f) * usageModifier;

        coreHealth = Mathf.Clamp(coreHealth, 0.0f, maxCoreHealth);
        coreUsage = Mathf.Clamp(coreUsage, 0.0f, maxCoreUsage);

        UpdateUI();

        coreUsage = 0;
    }

    private void UpdateUI()
    {
        coreHealthSlider.maxValue = maxCoreHealth;
        coreHealthSlider.value = Mathf.Lerp(coreHealthSlider.value, coreHealth, 2.5f * Time.deltaTime);
    }
}
