using Assets.Source.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICargoComponent : MonoBehaviour
{
    public Ore[] ores;
    public GameObject oreUIPrefab;
    public RectTransform contentPanel;
    public Text statusText;
    public Dictionary<Ore, UIOreComponent> oreUIs;

    private int totalOreCount = 0;
    private CanvasGroup canvasGroup;
    private ShipCargoComponent shipCargoComponent;

    private void Awake()
    {
        oreUIs = new Dictionary<Ore, UIOreComponent>();

        canvasGroup = contentPanel.gameObject.GetComponent<CanvasGroup>();
        shipCargoComponent = FindObjectOfType<ShipCargoComponent>();
    }

    private void Start()
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (Ore ore in ores)
        {
            GameObject oreUIObject = Instantiate(oreUIPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            oreUIObject.transform.SetParent(contentPanel, false);

            UIOreComponent ui = oreUIObject.GetComponent<UIOreComponent>();
            ui.Ore = ore;

            oreUIs[ore] = ui;
        }
    }

    private void Update()
    {
        statusText.gameObject.SetActive(shipCargoComponent.IsCargoHoldFull());

        foreach (Ore ore in ores)
        {
            UIOreComponent ui = oreUIs[ore];

            int count = shipCargoComponent.GetCount(ore);

            ui.gameObject.SetActive(count != 0);

            if (ui.Count != count)
            {
                ui.Count = count;
            }

            totalOreCount += count;
        }

        canvasGroup.alpha = (totalOreCount > 0) ? 1 : 0;
        totalOreCount = 0;
    }
}
