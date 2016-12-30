using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputComponent : MonoBehaviour
{
    public Slider thrustSlider;
    public TurretComponent turretComponent;

    private Camera mainCamera;
    private ShipMovementComponent shipMovementComponent;

    private void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        shipMovementComponent = GetComponent<ShipMovementComponent>();
    }

    private void Update ()
    {
        shipMovementComponent.thrust = thrustSlider.value;

        if (Input.GetKey(KeyCode.Q))
        {
            turretComponent.isActive = true;
            turretComponent.targetPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f));
        }
        else
        {
            turretComponent.isActive = false;
        }
    }

}
