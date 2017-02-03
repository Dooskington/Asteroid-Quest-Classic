using Assets.Source.Data;
using CnControls;
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
    public UITutorial tutorialPanel;
    public Slider thrustSlider;
    public int credits;
    public float sellPriceModifier;
    public int score;
    public List<Upgrade> Upgrades { get; set; }
    public GameObject explosionPrefab;
    public GameObject navigator;

    private Quaternion targetRotation;
    private int totalCredts;
    private RaycastHit2D mouseRayHit;
    private ShipWeaponComponent shipWeapon;
    private ShipMovementComponent shipMovementComponent;
    private ShipReactorComponent shipReactor;
    private ShipDockingComponent shipDocking;
    private ShipDefenseComponent shipDefense;

    public void AddCredits(int amount)
    {
        credits += amount;
        totalCredts += amount;
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

        totalCredts = credits;

        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Confined;

        tutorialPanel.Open();
    }

    private void Update()
    {
        if (gameOverPanel.isOpen)
        {
            return;
        }

        if (CnInputManager.GetButton("Fire"))
        {
            shipWeapon.Fire();
        }

        navigator.SetActive(Input.GetKey(KeyCode.Tab));
        cargoPanel.SetActive(!shipDocking.IsDocked);

        if (shipDocking.IsDocked)
        {
            return;
        }

        if (Time.timeScale != 0)
        {
            Vector3 inputVector = new Vector3(-CnInputManager.GetAxis("Vertical"), CnInputManager.GetAxis("Horizontal"));
            shipMovementComponent.thrust = inputVector.magnitude;
            if (inputVector.magnitude > 0.0f)
            {
                var angle = (Mathf.Atan2(inputVector.y, inputVector.x) - 0.0f) * Mathf.Rad2Deg;
                targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            Debug.Log(inputVector);

            if (targetRotation != Quaternion.identity)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10.0f * Time.deltaTime);
            }
        }

        score = totalCredts;
        foreach(Upgrade upgrade in Upgrades)
        {
            score += upgrade.cost;
        }

        if (!gameOverPanel.isOpen)
        {
            if (shipReactor.coreHealth <= 0)
            {
                gameOverPanel.Open("You have run out of power, and are stranded.", score);
            }
            else if (shipDefense.hull <= 0)
            {
                gameOverPanel.Open("You have perished in the vacuum of space.", score);

                GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
                Destroy(explosion, 5.0f);
                Destroy(gameObject);
            }
        }
    }

    private void LateUpdate()
    {
        if (navigator)
        {
            navigator.transform.position = transform.position;

            Vector3 direction = (Vector3.zero - navigator.transform.position).normalized;

            float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + 90.0f;
            Quaternion q = Quaternion.AngleAxis(angle, navigator.transform.forward);
            navigator.transform.rotation = Quaternion.Slerp(navigator.transform.rotation, q, Time.deltaTime * 10.0f);
        }
    }
}
