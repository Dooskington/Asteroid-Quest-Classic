using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapBlipComponent : MonoBehaviour
{
    public MapDataComponent mapData;
    public Sprite blipSprite;
    public float blipScale = 1.0f;
    public Vector2 blipPivot = new Vector2(0.5f, 0.5f);
    public Image BlipUIImage { get; set; }

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
            Destroy(BlipUIImage.gameObject);
            mapData.blipList.Remove(this);
        }
    }
}
