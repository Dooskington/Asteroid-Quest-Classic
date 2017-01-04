using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapUIComponent : MonoBehaviour, IPointerClickHandler
{
    public MapDataComponent mapData;
    public float mapScale = 5.0f;

    public GameObject player;
    public RectTransform mapRect;
    public GameObject blipPrefab;

    private PlayerInputComponent playerComponent;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(mapRect, eventData.position))
        {
            Vector2 localPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, eventData.position, null, out localPos);

            playerComponent.SetDestination(localPos / mapScale);
        }
    }

    private void Awake()
    {
        mapData = FindObjectOfType<MapDataComponent>();
        if (!mapData)
        {
            Debug.LogError(name + ": No MapData was provided!");
        }

        playerComponent = player.GetComponent<PlayerInputComponent>();
    }

    private void Update()
    {
        UpdateBlips();
    }

    private void UpdateBlips()
    {
        foreach (MapBlipComponent blip in mapData.blipList)
        {
            if (blip.BlipUIImage == null)
            {
                CreateUIBlip(blip);
            }

            blip.BlipUIImage.rectTransform.anchoredPosition = RectTransformUtility.WorldToScreenPoint(null, blip.transform.position) * mapScale;
            blip.BlipUIImage.rectTransform.rotation = blip.transform.rotation;
        }
    }

    private void CreateUIBlip(MapBlipComponent blip)
    {
        GameObject blipObject = Instantiate(blipPrefab, transform) as GameObject;
        blipObject.name = blip.name;
        blip.BlipUIImage = blipObject.GetComponent<Image>();
        blip.BlipUIImage.sprite = blip.blipSprite;
        blip.BlipUIImage.rectTransform.pivot = blip.blipPivot;
        blip.BlipUIImage.rectTransform.localScale = new Vector2(1.0f, 1.0f) * blip.blipScale;
    }
}

