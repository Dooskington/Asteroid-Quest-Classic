using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileComponent : MonoBehaviour
{
    public GameObject owner;
    public float moveSpeed = 200.0f;
    public float lifetimeSeconds = 2.0f;
    public float damage = 1;

    private Rigidbody2D rigidbodyComponent;

    private void Awake()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, lifetimeSeconds);

        rigidbodyComponent.velocity = -transform.up * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Station"))
        {
            return;
        }

        Destroy(gameObject);
    }
}
