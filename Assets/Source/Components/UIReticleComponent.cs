using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIReticleComponent : MonoBehaviour
{
    private Transform target;
    public Transform Target
    {
        get
        {
            return target;
        }

        set
        {
            target = value;
            UpdateTransform();
            gameObject.SetActive(target != null);
        }
    }

    public bool isLocked;
    public float rotationSpeed;

    private void Update()
    {
        UpdateTransform();
    }

    private void UpdateTransform()
    {
        if (!target)
        {
            return;
        }

        transform.position = Camera.main.WorldToScreenPoint(target.position);

        if (isLocked)
        {
            transform.Rotate(-Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
    }
}
