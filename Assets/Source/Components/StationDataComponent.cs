using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationDataComponent : MonoBehaviour
{
    public List<string> names = new List<string>();

    public string TakeName()
    {
        string name = "Station";

        if (names.Count > 0)
        {
            int index = Random.Range(0, names.Count);
            name = names[index];
            names.RemoveAt(index);
        }

        return name;
    }
}
