using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public Vector3 direction = Vector3.right;
    public float speed = 10.0f;

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
