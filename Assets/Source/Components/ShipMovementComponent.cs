using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovementComponent : MonoBehaviour
{
    public float thrust = 0.0f;
    public float moveSpeed = 100.0f;
    public float rotationSpeed = 180.0f;
    public float maxSpeed = 10.0f;

    private Rigidbody2D rigidbodyComponent;

	private void Awake()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (thrust > 0.0f)
        {
            rigidbodyComponent.AddForce(transform.right * (thrust * moveSpeed) * Time.deltaTime);
        }

        if (thrust > 0.0f)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(0.0f, 0.0f, rotationSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(0.0f, 0.0f, -rotationSpeed * Time.deltaTime);
            }
        }
    }

}
