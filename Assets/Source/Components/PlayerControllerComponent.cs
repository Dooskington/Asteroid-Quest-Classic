using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerControllerComponent : MonoBehaviour
{
    public GameObject mapPanel;
    public GameObject statsPanel;
    public GameObject cargoPanel;
    public Slider thrustSlider;
    public int credits;
    public int score;

    private RaycastHit2D mouseRayHit;
    private ShipWeaponComponent shipWeapon;
    private ShipMovementComponent shipMovementComponent;
    private ShipReactorComponent shipReactor;
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

    private void Awake()
    {
        shipWeapon = GetComponent<ShipWeaponComponent>();
        shipMovementComponent = GetComponent<ShipMovementComponent>();
        shipReactor = GetComponent<ShipReactorComponent>();
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
        statsPanel.SetActive(shipDocking.IsDocked);
        cargoPanel.SetActive(!shipDocking.IsDocked);

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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
