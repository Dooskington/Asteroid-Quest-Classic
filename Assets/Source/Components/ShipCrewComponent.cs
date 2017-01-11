using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipCrewComponent : MonoBehaviour
{
    public int hunger = 100;
    public int maxHunger = 100;
    public int hungerAmount = 5;
    public float hungerFrequency = 30.0f;
    public Slider hungerSlider;

    private float lastHungerTime;

    public void Feed()
    {
        lastHungerTime = Time.time;
        hunger = maxHunger;
    }

    private void Update()
    {
        if ((Time.time - lastHungerTime) >= hungerFrequency)
        {
            AddHunger();
        }

        hunger = Mathf.Clamp(hunger, 0, maxHunger);

        UpdateUI();
    }

    private void UpdateUI()
    {
        hungerSlider.maxValue = maxHunger;
        hungerSlider.value = Mathf.Lerp(hungerSlider.value, hunger, 2.5f * Time.deltaTime);
    }

    private void AddHunger()
    {
        lastHungerTime = Time.time;
        hunger -= hungerAmount;
    }
}
