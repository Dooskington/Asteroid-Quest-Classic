using Assets.Source.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipCargoComponent : MonoBehaviour
{
    public float pickupRadius = 2.5f;
    public int cargoHoldSize = 20;
    public bool isTransmutationEnabled = false;
    public float transmuteFrequency = 5.0f;
    public float transmutePowerUsage = 5.0f;
    public Dictionary<Ore, int> Ores { get; set; }

    private float lastTransmuteTime;
    private PlayerControllerComponent player;
    private ShipReactorComponent shipReactor;

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
        player = GetComponent<PlayerControllerComponent>();
        shipReactor = GetComponent<ShipReactorComponent>();
    }

    private void LateUpdate()
    {
        PickupOres();

        if (isTransmutationEnabled && (Ores.Count > 0))
        {
            if (transmutePowerUsage > 0)
            {
                shipReactor.UsePower(transmutePowerUsage);
            }

            if ((Time.time - lastTransmuteTime) >= transmuteFrequency)
            {
                List<Ore> oreList = new List<Ore>(Ores.Keys);
                Ore ore = oreList[Random.Range(0, oreList.Count)];

                RemoveOre(ore);
                player.credits += ore.cost;

                lastTransmuteTime = Time.time;
            }
        }
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
