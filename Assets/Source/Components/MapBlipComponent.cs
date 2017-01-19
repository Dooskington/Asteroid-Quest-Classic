using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapBlipComponent : MonoBehaviour
{
    public MapDataComponent mapData;
    public string blipName;
    public Sprite blipSprite;
    public float blipScale = 1.0f;
    public Vector2 blipPivot = new Vector2(0.5f, 0.5f);
    public Image BlipUIImage { get; set; }
    public Text BlipUIText { get; set; }
    public bool rotate = false;

    private void Awake()
    {
        mapData = FindObjectOfType<MapDataComponent>();
        if (!mapData)
        {
            Debug.LogError(name + ": No MapData was provided!");
        }
    }

    private void Start()
    {
        if (mapData)
        {
            mapData.blipList.Add(this);
        }
    }

    private void OnDestroy()
    {
        if (mapData)
        {
            if (BlipUIImage)
            {
                Destroy(BlipUIImage.gameObject);
            }

            if (BlipUIText)
            {
                Destroy(BlipUIText.gameObject);
            }

            mapData.blipList.Remove(this);
        }
    }
}
