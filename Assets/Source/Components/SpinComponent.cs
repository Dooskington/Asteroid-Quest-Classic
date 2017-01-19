using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinComponent : MonoBehaviour
{
    public float speed = 10.0f;

    private void Update()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, speed * Time.deltaTime));
    }
}
