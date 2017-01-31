using Assets.Source.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipCargoComponent : MonoBehaviour
{
    public float pickupRadius = 2.5f;
    public int cargoHoldSize = 20;

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

        int newAmount = currentCount - amount;
        if (newAmount == 0)
        {
            Ores.Remove(ore);
        }
        else
        {
            Ores[ore] = currentCount - amount;
        }

        return true;
    }

    public int GetCount(Ore ore)
    {
        int count = 0;
        Ores.TryGetValue(ore, out count);

        return count;
    }

    public bool IsCargoHoldFull()
    {
        return Ores.Values.Sum() >= cargoHoldSize;
    }

    private void Awake()
    {
        Ores = new Dictionary<Ore, int>();
    }

    private void LateUpdate()
    {
        PickupOres();
    }

    private void PickupOres()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pickupRadius);
        foreach (Collider2D collider in colliders)
        {
            if (IsCargoHoldFull())
            {
                return;
            }

            GameObject item = collider.gameObject;
            if (item.CompareTag("Ore"))
            {
                Vector3 direction = transform.position - item.transform.position;
                item.GetComponent<Rigidbody2D>().AddForce(direction.normalized * 25.0f);

                float distance = Vector3.Distance(transform.position, item.transform.position);
                if (distance <= 0.65f)
                {
                    OreComponent oreComponent = item.GetComponent<OreComponent>();
                    AddOre(oreComponent.Ore);
                    Destroy(item);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}
