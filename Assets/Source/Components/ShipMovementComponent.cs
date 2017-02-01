using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovementComponent : MonoBehaviour
{
    public float thrust = 0.0f;
    public float maxThrust = 1.0f;
    public float moveSpeed = 100.0f;
    public float maxSpeed = 10.0f;
    public float rotationSpeed = 90.0f;
    public float powerUsage = 25.0f;
    public ParticleSystem[] trails;

    private Rigidbody2D rigidbodyComponent;
    private AudioSource audioSourceComponent;
    private ShipReactorComponent shipReactor;

    public void Halt()
    {
        thrust = 0.0f;
        rigidbodyComponent.velocity = Vector3.zero;
        rigidbodyComponent.angularVelocity = 0.0f;
    }

    public void Turn(Vector3 direction)
    {
        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + 90.0f;
        Quaternion q = Quaternion.AngleAxis(angle, transform.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
    }

    public void TurnLeft()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    public void TurnRight()
    {
        transform.Rotate(-Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    private void Awake()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        audioSourceComponent = GetComponent<AudioSource>();
        shipReactor = GetComponent<ShipReactorComponent>();
    }

    private void Update()
    {
        if (thrust > 0.0f)
        {
            foreach (ParticleSystem trail in trails)
            {
                if (!trail.isPlaying)
                {
                    trail.Play();
                }
            }
        }
        else
        {
            foreach (ParticleSystem trail in trails)
            {
                if (trail.isPlaying)
                {
                    trail.Stop();
                }
            }
        }

        audioSourceComponent.volume = Mathf.Abs(thrust);
        thrust = Mathf.Clamp(thrust, -1.0f, 1.0f);

        if (shipReactor)
        {
            if (shipReactor.coreHealth <= 0)
            {
                thrust = 0.0f;
                return;
            }

            shipReactor.UsePower(Mathf.Abs(thrust) * powerUsage);
        }
    }

    private void FixedUpdate()
    {
        rigidbodyComponent.AddForce((-transform.up * (thrust * moveSpeed)) * Time.deltaTime);
        rigidbodyComponent.velocity = Vector2.ClampMagnitude(rigidbodyComponent.velocity, maxSpeed);
    }
}
