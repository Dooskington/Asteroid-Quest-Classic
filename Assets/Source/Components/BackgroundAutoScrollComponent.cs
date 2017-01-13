using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAutoScrollComponent : MonoBehaviour
{
    public GameObject[] backgrounds;
    public float[] speeds;

    private void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            GameObject bg = backgrounds[i];
            float speed = speeds[i];

            bg.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", 
                new Vector2((Time.time / 4.0f) * speed, (Time.time / 4.0f) * speed));
        }
    }
}
