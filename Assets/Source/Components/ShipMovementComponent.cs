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

    public Vector2 Destination
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

    private Quaternion m_startRotation;
    private Quaternion m_endRotation;
    private float m_moveStartTime;

    private GameObject m_destinationBlip;
    private GameObject m_mapLine;
    private LineRenderer m_mapLineRenderer;

    private Vector2 m_destination;
    private Rigidbody2D m_rigidbodyComponent;
    private AudioSource m_audioSourceComponent;
    private ShipReactorComponent m_reactorComponent;

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
        m_moveStartTime = Time.time;

        m_startRotation = transform.rotation;
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

        if (IsMoving)
        {
            m_reactorComponent.UsePower(m_thrust * 25.0f);
        }
    }

    private void FixedUpdate()
    {
        m_thrust = Mathf.Lerp(m_thrust, m_targetThrust, Time.deltaTime);
        m_thrust = Mathf.Clamp(m_thrust, 0.0f, m_maxThrust);

        m_rigidbodyComponent.velocity = Vector2.ClampMagnitude(m_rigidbodyComponent.velocity, m_maxSpeed);

        float distance = Vector3.Distance(transform.position, Destination);

        if (m_mapLineRenderer)
        {
            m_mapLineRenderer.SetPosition(0, transform.position);
            m_mapLineRenderer.material.SetTextureScale("_MainTex", new Vector2(distance * 3.5f, 1));
            m_mapLineRenderer.material.SetTextureOffset("_MainTex", new Vector2(-(distance * 3.5f), 1));
        }

        if (IsMoving)
        {
            m_rigidbodyComponent.AddForce(transform.right * (m_thrust * m_acceleration) * Time.deltaTime);

            float elapsedTime = Time.time - m_moveStartTime;
            float percentage = elapsedTime / m_rotationSpeed;

            Vector2 direction = -((new Vector2(transform.position.x, transform.position.y) - Destination).normalized);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            m_startRotation = transform.rotation;
            m_endRotation = Quaternion.Euler(0.0f, 0.0f, angle);

            transform.rotation = Quaternion.Slerp(m_startRotation,
                m_endRotation,
                percentage);

            if (distance <= 4.0f)
            {
                m_targetThrust = 0.5f;
            }

            if (distance <= 1.5f)
            {
                IsMoving = false;
                m_targetThrust = 0.0f;

                //Destroy(m_mapBlip);
                //Destroy(m_mapLine);
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

    private Vector3 GetRelativeAngles(Vector3 angles)
    {
        Vector3 relativeAngles = angles;

        if (relativeAngles.x > 180f)
        {
           relativeAngles.x -= 360f;
        }

        if (relativeAngles.y > 180f)
        {
           relativeAngles.y -= 360f;
        }

        if (relativeAngles.z > 180f)
        {
           relativeAngles.z -= 360f;
        }

        return relativeAngles;
    }
}
