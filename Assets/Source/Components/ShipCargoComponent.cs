using Assets.Source.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCargoComponent : MonoBehaviour
{
    public Dictionary<Ore, int> Ores { get; set; }

    public void AddOre(Ore ore, int amount = 1)
    {
        int currentCount = GetCount(ore);
        Ores[ore] = currentCount + amount;
    }

    public bool RemoveOre(Ore ore, int amount = 1)
    {
        int currentCount = GetCount(ore);

        if ((currentCount - amount) < 0)
        {
            return false;
        }

        Ores[ore] = currentCount - amount;
        return true;
    }

    public int GetCount(Ore ore)
    {
        int count = 0;
        Ores.TryGetValue(ore, out count);

        return count;
    }

    private void Awake()
    {
        Ores = new Dictionary<Ore, int>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Ore"))
        {
            OreComponent oreComponent = collider.GetComponent<OreComponent>();
            AddOre(oreComponent.Ore);

            Destroy(collider.gameObject);
        }
    }
}
