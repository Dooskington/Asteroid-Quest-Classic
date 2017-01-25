using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITurretStatusComponent : MonoBehaviour
{
    public ShipWeaponComponent turretComponent;
    public Color activeColor;
    public Color inactiveColor;

    private Text textComponent;

    private void Awake()
    {
        textComponent = GetComponent<Text>();
    }

    private void Update()
    {
        textComponent.text = "Active";
        textComponent.color = activeColor;

        /*
		if (turretComponent.isActive)
        {
            textComponent.text = "Active";
            textComponent.color = activeColor;
        }
        else
        {
            textComponent.text = "Inactive";
            textComponent.color = inactiveColor;
        }
        */
    }
}
