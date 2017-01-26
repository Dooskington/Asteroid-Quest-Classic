using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour
{
    public float lifetimeSeconds;

    private void Awake()
    {
        Destroy(gameObject, lifetimeSeconds);
    }
}
