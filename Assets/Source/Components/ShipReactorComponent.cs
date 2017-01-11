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

    public Slider coreHealthSlider;
    public Slider coreUsageSlider;

    public void UsePower(float amount)
    {
        coreUsage += amount;
    }

    private void Update()
    {
        coreHealth -= (coreUsage / 5000.0f);

        coreHealth = Mathf.Clamp(coreHealth, 0.0f, maxCoreHealth);
        coreUsage = Mathf.Clamp(coreUsage, 0.0f, maxCoreUsage);

        UpdateUI();

        coreUsage = 0.0f;
    }

    private void UpdateUI()
    {
        coreHealthSlider.maxValue = maxCoreHealth;
        coreUsageSlider.maxValue = maxCoreUsage;

        coreHealthSlider.value = Mathf.Lerp(coreHealthSlider.value, coreHealth, Time.deltaTime);
        coreUsageSlider.value = Mathf.Lerp(coreUsageSlider.value, coreUsage, Time.deltaTime);
    }
}
