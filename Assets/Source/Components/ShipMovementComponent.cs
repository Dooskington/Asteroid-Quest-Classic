using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovementComponent : MonoBehaviour
{
    public float thrust = 0.0f;
    public float m_moveSpeed = 100.0f;
    public float rotationSpeed = 180.0f;
    public float maxSpeed = 10.0f;
    public GameObject mapBlipPrefab;
    public GameObject mapLinePrefab;

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
            IsMoving = true;
            CreateDestinationLine();
        }
    }

    private bool isRotating = false;
    private Quaternion startRotation;
    private Quaternion endRotation;
    private float rotationStartTime;

    private GameObject mapBlip;
    private GameObject mapLine;
    private LineRenderer mapLineRenderer;

    private Vector2 m_destination;
    private Rigidbody2D m_rigidbodyComponent;

    public void Halt()
    {
        thrust = 0.0f;
        m_rigidbodyComponent.velocity = Vector3.zero;
    }

    public void BeginOrientation()
    {
        Vector2 direction = -((new Vector2(transform.position.x, transform.position.y) - Destination).normalized);
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            isRotating = true;
            rotationStartTime = Time.time;

            startRotation = transform.rotation;
            endRotation = Quaternion.Euler(0.0f, 0.0f, angle);
        }
    }

	private void Awake()
    {
        m_rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, Destination);

        if (mapLineRenderer)
        {
            mapLineRenderer.SetPosition(0, transform.position);
            mapLineRenderer.material.SetTextureScale("_MainTex", new Vector2(distance * 3.5f, 1));
            mapLineRenderer.material.SetTextureOffset("_MainTex", new Vector2(-(distance * 3.5f), 1));
        }

        if (isRotating)
        {
            float elapsedTime = Time.time - rotationStartTime;
            float rotationTime = distance / rotationSpeed;
            float percentage = elapsedTime / rotationTime;

            transform.rotation = Quaternion.Lerp(startRotation, 
                endRotation,
                percentage);

            if (percentage >= 1.0f)
            {
                //isRotating = false;
            }
        }

        /*
        if (thrust > 0.0f)
        {
            m_rigidbodyComponent.AddForce(transform.right * (thrust * moveSpeed) * Time.deltaTime);
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
        */

        if (IsMoving)
        {
            thrust = Mathf.Lerp(thrust, 1.0f, 1.0f * Time.deltaTime);

            Debug.DrawLine(transform.position, new Vector3(Destination.x, Destination.y, 0.0f), Color.green);

            m_rigidbodyComponent.AddForce(transform.right * (thrust * m_moveSpeed) * Time.deltaTime);

            Vector2 direction = -((new Vector2(transform.position.x, transform.position.y) - Destination).normalized);
            if (direction != Vector2.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                startRotation = transform.rotation;
                endRotation = Quaternion.Euler(0.0f, 0.0f, angle);

                /*
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.Lerp(transform.rotation, 
                    Quaternion.Euler(0.0f, 0.0f, angle), 
                    rotationSpeed * Time.deltaTime);
                    */
            }

            if (distance <= 3.0f)
            {
                thrust = 0.25f;
            }

            if (distance <= 1.0f)
            {
                IsMoving = false;
            }
        }
    }

    /*
    private void FixedUpdate()
    {
        if (IsMoving)
        {
            Vector3 direction = (Destination - transform.position).normalized;
            rigidbodyComponent.velocity = direction * speed;

            if (Vector3.Distance(transform.position, Destination) <= 0.25f)
            {
                IsMoving = false;
            }
        }
        else
        {
            animatorController.SetBool("isMoving", false);
            rigidbodyComponent.velocity *= 0.9f;
        }
    }
    */


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

    private void CreateDestinationLine()
    {
        if (mapLine)
        {
            DestroyImmediate(mapLine);
        }

        if (mapBlip)
        {
            DestroyImmediate(mapBlip);
        }

        mapBlip = Instantiate(mapBlipPrefab, new Vector3(Destination.x, Destination.y), Quaternion.identity) as GameObject;
        mapLine = Instantiate(mapLinePrefab, Vector3.zero, Quaternion.identity) as GameObject;

        mapLineRenderer = mapLine.GetComponent<LineRenderer>();
        mapLineRenderer.SetPosition(0, transform.position);
        mapLineRenderer.SetPosition(1, new Vector3(Destination.x, Destination.y, 0.0f));
        mapLineRenderer.enabled = true;
    }
}
