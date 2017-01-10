using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovementComponent : MonoBehaviour
{
    public float m_targetThrust = 0.0f;
    public float m_thrust = 0.0f;
    public float m_maxThrust = 1.0f;
    public float m_acceleration = 100.0f;
    public float m_maxSpeed = 10.0f;
    public float m_rotationSpeed = 90.0f;
    public float m_powerUsage = 25.0f;

    private Rigidbody2D m_rigidbodyComponent;
    private AudioSource m_audioSourceComponent;
    private ShipReactorComponent m_reactorComponent;

    public void Halt()
    {
        m_targetThrust = 0.0f;
        m_thrust = 0.0f;
        m_rigidbodyComponent.velocity = Vector3.zero;
        m_rigidbodyComponent.angularVelocity = 0.0f;
    }

    private void Awake()
    {
        m_rigidbodyComponent = GetComponent<Rigidbody2D>();
        m_audioSourceComponent = GetComponent<AudioSource>();
        m_reactorComponent = GetComponent<ShipReactorComponent>();
    }

    private void Update()
    {
        m_targetThrust = Mathf.Clamp(m_targetThrust, 0.0f, m_maxThrust);
        m_thrust = Mathf.Lerp(m_thrust, m_targetThrust, 2.5f * Time.deltaTime);
        m_thrust = Mathf.Clamp(m_thrust, 0.0f, m_maxThrust);

        m_audioSourceComponent.volume = m_thrust;
        m_reactorComponent.UsePower(m_thrust * m_powerUsage);

        if (Input.GetKeyDown(KeyCode.W))
        {
            m_targetThrust += 0.35f;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            m_targetThrust -= 0.35f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward, (m_thrust * m_rotationSpeed) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward, (m_thrust * m_rotationSpeed) * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        m_rigidbodyComponent.AddForce((transform.right * (m_thrust * m_acceleration)) * Time.deltaTime);
        m_rigidbodyComponent.velocity = Vector2.ClampMagnitude(m_rigidbodyComponent.velocity, m_maxSpeed);
    }
}
