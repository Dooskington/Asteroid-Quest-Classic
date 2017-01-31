using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UITooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltipPrefab;
    public string title = "Title";
    public string description = "Description";
    public bool isTitleEnabled = true;
    public float fadeTime = 0.25f;
    public float delay = 0.0f;

    private bool isWaiting = false;
    private float tooltipStartTime;
    private GameObject tooltipObject;
    private Transform canvas;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltipObject != null)
        {
            return;
        }

        if (!isWaiting)
        {
            tooltipStartTime = Time.time;
            isWaiting = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isWaiting = false;
        Destroy(tooltipObject);
    }

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>().transform;
    }

    private void Update()
    {
        if (isWaiting && (tooltipObject == null))
        {
            if ((Time.time - tooltipStartTime) >= delay)
            {
                tooltipObject = Instantiate(tooltipPrefab, Input.mousePosition, Quaternion.identity) as GameObject;
                tooltipObject.transform.SetParent(canvas, false);

                PopulateTooltip();

                isWaiting = false;
            }
        }

        if (tooltipObject == null)
        {
            return;
        }

        tooltipObject.transform.position = Input.mousePosition;
    }

    private void OnDisable()
    {
        if (tooltipObject)
        {
            Destroy(tooltipObject);
        }
    }

    private void PopulateTooltip()
    {
        Text titleText = tooltipObject.transform.Find("Title").gameObject.GetComponent<Text>();
        Text descriptionText = tooltipObject.transform.Find("Description").gameObject.GetComponent<Text>();

        if (titleText)
        {
            titleText.text = title;
        }

        if (descriptionText)
        {
            descriptionText.text = description;
        }

        titleText.enabled = isTitleEnabled;

        Juice.Instance.FadeGroup(tooltipObject.GetComponent<CanvasGroup>(), fadeTime, 1.0f);
    }
}
