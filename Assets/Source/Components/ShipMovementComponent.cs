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
    public float m_rotationSpeed = 0.5f;

    public GameObject m_mapLinePrefab;

    public delegate void DestinationChanged();
    public event DestinationChanged OnDestinationChanged;

    public bool IsMoving { get; set; }

    public Transform OrbitTarget { get; set; }

    private Vector3 m_destination;
    public Vector3 Destination
    {
        get
        {
            return m_destination;
        }

        set
        {
            m_destination = value;
            BeginMovement();

            if (OnDestinationChanged != null)
            {
                OnDestinationChanged();
            }
        }
    }

    private Rigidbody2D m_rigidbodyComponent;
    private AudioSource m_audioSourceComponent;
    private ShipReactorComponent m_reactorComponent;

    public float orbitRadius = 5.0f;

    public void Halt()
    {
        IsMoving = false;

        m_thrust = 0.0f;
        m_rigidbodyComponent.velocity = Vector3.zero;
        m_rigidbodyComponent.angularVelocity = 0.0f;
    }

    public void BeginMovement()
    {
        IsMoving = true;
        m_targetThrust = m_maxThrust;
    }

    private void Awake()
    {
        m_rigidbodyComponent = GetComponent<Rigidbody2D>();
        m_audioSourceComponent = GetComponent<AudioSource>();
        m_reactorComponent = GetComponent<ShipReactorComponent>();
    }

    private void Update()
    {
        m_audioSourceComponent.volume = m_thrust;
        m_reactorComponent.UsePower(m_thrust * 25.0f);
    }

    private void FixedUpdate()
    {
        // Thrust

        m_thrust = Mathf.Lerp(m_thrust, m_targetThrust, Time.deltaTime);
        m_thrust = Mathf.Clamp(m_thrust, 0.0f, m_maxThrust);
        m_rigidbodyComponent.AddForce(transform.right * (m_thrust * m_acceleration) * Time.deltaTime);
        m_rigidbodyComponent.velocity = Vector2.ClampMagnitude(m_rigidbodyComponent.velocity, m_maxSpeed);

        float distance = Vector3.Distance(transform.position, Destination);

        // Orbit Calculation

        if (OrbitTarget)
        {
            Vector3 direction = (transform.position - OrbitTarget.transform.position).normalized;
            Vector3 perpendicularDirection = new Vector3(direction.y, -direction.x, 0.0f);

            Vector3 shipPoint = OrbitTarget.position + (direction * orbitRadius);
            Vector3 midPointDirection = (direction + perpendicularDirection).normalized;
            Vector3 midArcPoint = OrbitTarget.position + (midPointDirection * orbitRadius);

            Debug.DrawLine(OrbitTarget.position, shipPoint, Color.cyan);

            Destination = midArcPoint;
        }

        // Steering

        if (IsMoving)
        {
            Vector3 direction = -((transform.position - Destination).normalized);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.Euler(0.0f, 0.0f, angle),
                m_rotationSpeed * Time.deltaTime);

            if (distance <= 3.0f)
            {
                m_targetThrust = 0.5f;
            }

            if (distance <= 1.5f)
            {
                IsMoving = false;
                m_targetThrust = 0.0f;
            }
        }
        else
        {
            if (m_thrust <= 0.05f)
            {
                Halt();
            }
        }
    }
}
