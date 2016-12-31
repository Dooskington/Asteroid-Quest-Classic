using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretStatusUIComponent : MonoBehaviour
{
    public TurretComponent turretComponent;
    public Color activeColor;
    public Color inactiveColor;

    private Text textComponent;

    private void Awake()
    {
        textComponent = GetComponent<Text>();
    }

    private void Update()
    {
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
	}
}
