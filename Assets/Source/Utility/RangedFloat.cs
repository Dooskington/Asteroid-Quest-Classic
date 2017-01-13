using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RangedFloat
{
    public float rangeStart = 1.0f;
    public float rangeEnd = 1.0f;

    public float GetRandomValue()
    {
        return Random.Range(rangeStart, rangeEnd);
    }
}