using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatsComponent : MonoBehaviour
{
    public Text creditsText;
    public Text cargoCapacityText;
    public Text weaponDamageText;
    public Text pickupRadiusText;
    public Text maxHullText;
    public Text maxShieldText;
    public Text maxReactorText;
    public Text speedText;

    private PlayerControllerComponent player;
    private ShipCargoComponent shipCargo;
    private ShipWeaponComponent shipWeapon;
    private ShipDefenseComponent shipDefense;
    private ShipReactorComponent shipReactor;
    private ShipMovementComponent shipMovement;

    private void Awake()
    {
        player = FindObjectOfType<PlayerControllerComponent>();
        shipCargo = player.GetComponent<ShipCargoComponent>();
        shipWeapon = player.GetComponent<ShipWeaponComponent>();
        shipDefense = player.GetComponent<ShipDefenseComponent>();
        shipReactor = player.GetComponent<ShipReactorComponent>();
        shipMovement = player.GetComponent<ShipMovementComponent>();
    }

    private void Update()
    {
        creditsText.text = player.credits.ToString();
        cargoCapacityText.text = shipCargo.cargoHoldSize.ToString();
        weaponDamageText.text = shipWeapon.damage.ToString("0.0");
        pickupRadiusText.text = shipCargo.pickupRadius.ToString("0.0");
        maxHullText.text = ((int) shipDefense.maxHull).ToString();
        maxShieldText.text = ((int) shipDefense.maxShield).ToString();
        maxReactorText.text = ((int) shipReactor.maxCoreHealth).ToString();
        speedText.text = ((int) shipMovement.moveSpeed).ToString();
    }
}
