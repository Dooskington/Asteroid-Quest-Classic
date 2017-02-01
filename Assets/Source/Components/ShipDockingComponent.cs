using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipDockingComponent : MonoBehaviour
{
    public Text dockingIndicatorText;
    public UIStationComponent stationPanel;

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
        }
    }

    public void Dock()
    {
        IsDocked = true;
        IsDockable = false;

        shipMovementComponent.Halt();

        stationPanel.Open();

        WorldManager world = FindObjectOfType<WorldManager>();
        world.Regenerate();
    }

    public void Undock()
    {
        IsDocked = false;
        IsDockable = true;

        stationPanel.Close();
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
