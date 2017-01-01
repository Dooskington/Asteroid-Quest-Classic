using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipDockingComponent : MonoBehaviour
{
    public Text dockingIndicatorText;
    public GameObject stationPanel;

    private ShipMovementComponent shipMovementComponent;

    private bool isDockable = false;
    public bool IsDockable
    {
        get
        {
            return isDockable;
        }

        set
        {
            isDockable = value;
            dockingIndicatorText.gameObject.SetActive(value);
        }
    }

    private bool isDocked = false;
    public bool IsDocked
    {
        get
        {
            return isDocked;
        }

        set
        {
            isDocked = value;
            stationPanel.gameObject.SetActive(value);
        }
    }

    public void Dock()
    {
        IsDocked = true;
        IsDockable = false;

        shipMovementComponent.Halt();
    }

    public void Undock()
    {
        IsDocked = false;
        IsDockable = true;
    }

    private void Awake()
    {
        shipMovementComponent = GetComponent<ShipMovementComponent>();
    }

    private void Update()
    {
        if (!IsDockable)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Dock();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dock"))
        {
            IsDockable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Dock"))
        {
            IsDockable = false;
        }
    }
}
