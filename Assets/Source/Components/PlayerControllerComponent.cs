using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerControllerComponent : MonoBehaviour
{
    public GameObject mapPanel;
    public GameObject statsPanel;
    public GameObject questPanel;
    public UITargetComponent infoPanel;
    public Slider thrustSlider;
    public TurretComponent turretComponent;
    public int credits;
    public int score;

    private RaycastHit2D mouseRayHit;
    private ShipMovementComponent m_shipMovementComponent;

    public void AddCredits(int amount)
    {
        credits += amount;
    }

    public bool TakeCredits(int amount)
    {
        if ((credits - amount) < 0)
        {
            return false;
        }

        credits -= amount;
        return true;
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
            if (Input.GetMouseButton(0))
            {
                turretComponent.Fire();
            }
        }

        thrustSlider.value = m_shipMovementComponent.m_thrust;

        mapPanel.SetActive(Input.GetKey(KeyCode.Tab));
        statsPanel.SetActive(Input.GetKey(KeyCode.Tab));
        questPanel.SetActive(Input.GetKey(KeyCode.Tab));

        if (Input.GetKeyDown(KeyCode.Q))
        {
            turretComponent.isActive = !turretComponent.isActive;
        }

        turretComponent.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
