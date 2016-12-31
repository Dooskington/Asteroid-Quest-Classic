using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileComponent : MonoBehaviour
{
    public float moveSpeed = 200.0f;

    private Rigidbody2D rigidbodyComponent;

    private void Awake()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5.0f);

        //rigidbodyComponent.AddForce(transform.right * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
        //rigidbodyComponent.velocity = new Vector2(transform.right.x, transform.right.y) * (moveSpeed * Time.deltaTime);
    }

    private void Start()
    {
        rigidbodyComponent.AddForce(transform.right * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
    }

    /*
    private void FixedUpdate()
    {
        //rigidbodyComponent.AddForce(transform.right * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
    }
    */

}
