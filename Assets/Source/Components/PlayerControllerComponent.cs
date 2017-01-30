using Assets.Source.Data;
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
    public UIGameOver gameOverPanel;
    public Slider thrustSlider;
    public int credits;
    public int score;
    public List<Upgrade> Upgrades { get; set; }

    private RaycastHit2D mouseRayHit;
    private ShipWeaponComponent shipWeapon;
    private ShipMovementComponent shipMovementComponent;
    private ShipReactorComponent shipReactor;
    private ShipDockingComponent shipDocking;
    private ShipDefenseComponent shipDefense;

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
        Upgrades = new List<Upgrade>();

        shipWeapon = GetComponent<ShipWeaponComponent>();
        shipMovementComponent = GetComponent<ShipMovementComponent>();
        shipReactor = GetComponent<ShipReactorComponent>();
        shipDocking = GetComponent<ShipDockingComponent>();
        shipDefense = GetComponent<ShipDefenseComponent>();
    }

    private void Update()
    {
        if (Input.GetButton("Fire"))
        {
            shipWeapon.Fire();
        }

        thrustSlider.value = Mathf.Lerp(thrustSlider.value, shipMovementComponent.thrust, 2.5f * Time.deltaTime);

        //mapPanel.SetActive(Input.GetKey(KeyCode.Tab));
        cargoPanel.SetActive(!shipDocking.IsDocked);

        if (shipDocking.IsDocked)
        {
            return;
        }

        if (Time.timeScale != 0)
        {
            shipMovementComponent.thrust = Input.GetAxis("Vertical");
        }

        if (Input.GetKey(KeyCode.A))
        {
            shipMovementComponent.TurnLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            shipMovementComponent.TurnRight();
        }

        if (!gameOverPanel.isOpen && ((shipDefense.hull <= 0) || shipReactor.coreHealth <= 0))
        {
            gameOverPanel.Open();
        }
    }
}
