using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpin : MonoBehaviour
{
    public float minSpeed = -20.0f;
    public float maxSpeed =  20.0f;

    private float speed;

    private void Awake()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, speed * Time.deltaTime));
    }
}
