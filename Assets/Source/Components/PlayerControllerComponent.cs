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
    public int credits;
    public int score;

    private RaycastHit2D mouseRayHit;
    private ShipWeaponComponent shipWeapon;
    private ShipMovementComponent shipMovementComponent;
    private ShipReactorComponent shipReactor;
    private ShipCrewComponent shipCrew;
    private ShipDockingComponent shipDocking;

    public void AddCredits(int amount)
    {
        credits += amount;
    }

    public bool TakeCredits(int amount)
    {
        if (!HasCredits(amount))
        {
            return false;
        }

        credits -= amount;
        return true;
    }

    public bool HasCredits(int amount)
    {
        return (credits >= amount);
    }

    public void Recharge()
    {
        shipReactor.coreHealth = shipReactor.maxCoreHealth;
    }

    public void Feed()
    {
        shipCrew.Feed();
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
        shipWeapon = GetComponent<ShipWeaponComponent>();
        shipMovementComponent = GetComponent<ShipMovementComponent>();
        shipReactor = GetComponent<ShipReactorComponent>();
        shipCrew = GetComponent<ShipCrewComponent>();
        shipDocking = GetComponent<ShipDockingComponent>();
    }

    private void Update()
    {
        if (Input.GetButton("Fire"))
        {
            shipWeapon.Fire();
        }

        thrustSlider.value = Mathf.Lerp(thrustSlider.value, shipMovementComponent.thrust, 2.5f * Time.deltaTime);

        mapPanel.SetActive(Input.GetKey(KeyCode.Tab));
        statsPanel.SetActive(shipDocking.IsDocked || Input.GetKey(KeyCode.Tab));
        questPanel.SetActive(shipDocking.IsDocked || Input.GetKey(KeyCode.Tab));

        if (shipDocking.IsDocked)
        {
            return;
        }

        shipMovementComponent.thrust = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.A))
        {
            shipMovementComponent.TurnLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            shipMovementComponent.TurnRight();
        }
    }
}
