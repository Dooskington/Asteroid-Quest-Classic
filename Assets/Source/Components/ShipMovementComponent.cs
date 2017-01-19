using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovementComponent : MonoBehaviour
{
    public float targetThrust = 0.0f;
    public float thrust = 0.0f;
    public float maxThrust = 1.0f;
    public float acceleration = 100.0f;
    public float maxSpeed = 10.0f;
    public float rotationSpeed = 90.0f;
    public float powerUsage = 25.0f;

    private Rigidbody2D rigidbodyComponent;
    private AudioSource audioSourceComponent;
    private ShipReactorComponent shipReactor;

    public void Halt()
    {
        targetThrust = 0.0f;
        thrust = 0.0f;
        rigidbodyComponent.velocity = Vector3.zero;
        rigidbodyComponent.angularVelocity = 0.0f;
    }

    public void IncreaseThrust()
    {
        targetThrust += 0.35f;
    }

    public void DecreaseThrust()
    {
        targetThrust -= 0.35f;
    }

    public void TurnLeft()
    {
        transform.Rotate(Vector3.forward, (thrust * rotationSpeed) * Time.deltaTime);
    }

    public void TurnRight()
    {
        transform.Rotate(-Vector3.forward, (thrust * rotationSpeed) * Time.deltaTime);
    }

    private void Awake()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        audioSourceComponent = GetComponent<AudioSource>();
        shipReactor = GetComponent<ShipReactorComponent>();
    }

    private void Update()
    {
        audioSourceComponent.volume = thrust;

        targetThrust = Mathf.Clamp(targetThrust, 0.0f, maxThrust);
        thrust = Mathf.Lerp(thrust, targetThrust, 2.5f * Time.deltaTime);
        thrust = Mathf.Clamp(thrust, 0.0f, maxThrust);

        if (shipReactor.coreHealth <= 0)
        {
            targetThrust = 0.0f;
            return;
        }

        shipReactor.UsePower(thrust * powerUsage);
    }

    private void FixedUpdate()
    {
        rigidbodyComponent.AddForce((transform.right * (thrust * acceleration)) * Time.deltaTime);
        rigidbodyComponent.velocity = Vector2.ClampMagnitude(rigidbodyComponent.velocity, maxSpeed);
    }
}
