using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretComponent : MonoBehaviour
{
    public bool isActive = false;
    public Vector2 targetPosition = Vector2.zero;
    public GameObject laserSightObject;
	
	private void Update()
    {
        laserSightObject.SetActive(isActive);

        if (isActive)
        {
            float angleRad = Mathf.Atan2(targetPosition.y - transform.position.y, targetPosition.x - transform.position.x);
            float angleDeg = (180 / Mathf.PI) * angleRad;

            transform.rotation = Quaternion.Euler(0, 0, angleDeg);
        }
    }
}
