using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInputComponent : MonoBehaviour
{
    public GameObject mapPanel;
    public Slider thrustSlider;
    public TurretComponent turretComponent;

    private Camera mainCamera;
    private ShipMovementComponent shipMovementComponent;

    private void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        shipMovementComponent = GetComponent<ShipMovementComponent>();
    }

    private void Update()
    {
        if (shipMovementComponent.m_thrust != thrustSlider.value)
        {
            thrustSlider.value = shipMovementComponent.m_thrust;
        }

        mapPanel.SetActive(Input.GetKey(KeyCode.Tab));

        /*
        if (Input.GetKey(KeyCode.Q))
        {
            turretComponent.isActive = true;
            turretComponent.targetPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f));

            if (Input.GetMouseButtonDown(0))
            {
                turretComponent.Fire();
            }
        }
        else
        {
            turretComponent.isActive = false;
        }
        */
    }
}
