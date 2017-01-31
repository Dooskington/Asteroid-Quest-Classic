using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Data.Upgrades
{
    [CreateAssetMenu(menuName = "Data/Upgrade/Hull Capacity")]
    public class HullCapacity : Upgrade
    {
        public float capacityIncreasePercentage = 0.25f;

        public override void Apply(PlayerControllerComponent player)
        {
            base.Apply(player);

            ShipDefenseComponent shipDefense = player.GetComponent<ShipDefenseComponent>();
            shipDefense.maxHull += (shipDefense.maxHull * capacityIncreasePercentage);
            shipDefense.hull = shipDefense.maxHull;
        }
    }
}
