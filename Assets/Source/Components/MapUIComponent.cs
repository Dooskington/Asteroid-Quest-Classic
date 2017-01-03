using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapUIComponent : MonoBehaviour, IPointerClickHandler
{
    public GameObject player;
    public RectTransform mapRect;
    public GameObject blipPrefab;

    private ShipMovementComponent playerMovementComponent;
    private Dictionary<Transform, Image> blips = new Dictionary<Transform, Image>();

    public void OnPointerClick(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(mapRect, eventData.position))
        {
            Vector2 localPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, eventData.position, null, out localPos);

            Vector3 position = new Vector3(localPos.x, localPos.y, 0.0f);
            playerMovementComponent.Destination = position;
        }
    }

    public void AddBlip(Transform blipTransform, Sprite blipSprite)
    {
        GameObject blipObject = Instantiate(blipPrefab, transform) as GameObject;
        Image blipImage = blipObject.GetComponent<Image>();
    
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(null, blipTransform.position);

        blipImage.sprite = blipSprite;
        blipImage.rectTransform.anchoredPosition = screenPos;

        blips.Add(blipTransform, blipImage);
    }

    private void Awake()
    {
        playerMovementComponent = player.GetComponent<ShipMovementComponent>();
    }

    private void Update()
    {
        foreach (Transform blipTransform in blips.Keys)
        {
            Image blipImage = blips[blipTransform];

            blipImage.rectTransform.anchoredPosition = RectTransformUtility.WorldToScreenPoint(null, blipTransform.position);
        }
    }
}

