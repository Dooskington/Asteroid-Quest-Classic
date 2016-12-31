using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileComponent : MonoBehaviour
{
    public GameObject owner;
    public float moveSpeed = 200.0f;
    public float lifetimeSeconds = 2.0f;

    private Rigidbody2D rigidbodyComponent;

    private void Awake()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, lifetimeSeconds);
        rigidbodyComponent.AddForce(transform.right * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == owner.layer)
        {
            return;
        }

        Destroy(gameObject);
    }
}
