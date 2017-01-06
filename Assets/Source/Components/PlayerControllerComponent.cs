using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerControllerComponent : MonoBehaviour
{
    public GameObject mapPanel;
    public UITargetComponent infoPanel;
    public Slider thrustSlider;
    public TurretComponent turretComponent;

    private RaycastHit2D mouseRayHit;
    private ShipMovementComponent m_shipMovementComponent;

    public void SetDestination(Vector2 destination)
    {
        m_shipMovementComponent.Destination = destination;
    }

    public void Orbit(Transform target)
    {
        m_shipMovementComponent.OrbitTarget = target;
    }

    public void Target(Transform target)
    {
        ShipTargetingComponent targeting = GetComponent<ShipTargetingComponent>();
        if (targeting)
        {
            targeting.Target(target);
        }
    }

    public void ClearTarget()
    {
        ShipTargetingComponent targeting = GetComponent<ShipTargetingComponent>();
        if (targeting)
        {
            targeting.ClearTarget();
        }
    }

    public void LockTarget()
    {
        ShipTargetingComponent targeting = GetComponent<ShipTargetingComponent>();
        if (targeting)
        {
            targeting.LockTarget();
        }
    }

    public void UnlockTarget()
    {
        ShipTargetingComponent targeting = GetComponent<ShipTargetingComponent>();
        if (targeting)
        {
            targeting.UnlockTarget();
        }
    }

    private void Awake()
    {
        m_shipMovementComponent = GetComponent<ShipMovementComponent>();
    }

    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            mouseRayHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (Input.GetMouseButtonDown(0))
            {
                if (mouseRayHit.collider)
                {
                    Target(mouseRayHit.collider.transform);
                }
                else
                {
                    Target(null);
                }
            }
        }

        if (m_shipMovementComponent.m_thrust != thrustSlider.value)
        {
            thrustSlider.value = m_shipMovementComponent.m_thrust;
        }

        mapPanel.SetActive(Input.GetKey(KeyCode.Tab));

        if (Input.GetKeyDown(KeyCode.Q))
        {
            turretComponent.isActive = !turretComponent.isActive;
        }
    }
}
