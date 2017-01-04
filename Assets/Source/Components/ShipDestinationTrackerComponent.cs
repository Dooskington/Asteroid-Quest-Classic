using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDestinationTrackerComponent : MonoBehaviour
{
    public GameObject m_destinationBlipPrefab;

    private GameObject m_destinationObject;
    private ShipMovementComponent m_shipMovementComponent;

    private void Awake()
    {
        m_shipMovementComponent = GetComponent<ShipMovementComponent>();
        m_shipMovementComponent.OnDestinationChanged += OnDestinationChanged;
    }

    private void OnDestinationChanged()
    {
        if (m_destinationObject)
        {
            Destroy(m_destinationObject);
        }

        m_destinationObject = Instantiate(m_destinationBlipPrefab, 
            m_shipMovementComponent.Destination, 
            Quaternion.identity) as GameObject;
    }

    private void Update()
    {
        if (m_destinationObject)
        {
            float distance = Vector2.Distance(transform.position, m_destinationObject.transform.position);
            if (distance <= 1.5f)
            {
                Destroy(m_destinationObject);
            }
        }
    }
}
