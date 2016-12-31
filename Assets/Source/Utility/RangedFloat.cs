using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RangedFloat
{
    public float rangeStart;
    public float rangeEnd;

    public float GetRandomValue()
    {
        return Random.Range(rangeStart, rangeEnd);
    }
}