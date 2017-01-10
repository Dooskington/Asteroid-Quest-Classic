using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrollComponent : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject[] backgrounds;
    public float[] speeds;

    private void Update()
    {
        transform.position = playerObject.transform.position;

        for (int i = 0; i < backgrounds.Length; i++)
        {
            GameObject bg = backgrounds[i];
            float speed = speeds[i];
            bg.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(transform.position.x * speed, transform.position.y * speed));
        }
    }
}
