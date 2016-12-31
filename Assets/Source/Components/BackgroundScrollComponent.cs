using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrollComponent : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject backgroundObject;
    public GameObject foregroundObject;
    public float backgroundScrollSpeed = 1.0f;
    public float foregroundScrollSpeed = 0.5f;

    private Material backgroundMaterial;
    private Material foregroundMaterial;

    private void Awake()
    {
        backgroundMaterial = backgroundObject.GetComponent<Renderer>().material;
        foregroundMaterial = foregroundObject.GetComponent<Renderer>().material;
    }

    private void Update()
    {
        transform.position = playerObject.transform.position;

        backgroundMaterial.SetTextureOffset("_MainTex", new Vector2(transform.position.x * backgroundScrollSpeed, transform.position.y * backgroundScrollSpeed));
        foregroundMaterial.SetTextureOffset("_MainTex", new Vector2(transform.position.x * foregroundScrollSpeed, transform.position.y * foregroundScrollSpeed));
    }
}
