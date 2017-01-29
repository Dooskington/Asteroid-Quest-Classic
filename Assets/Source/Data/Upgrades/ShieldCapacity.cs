using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Data.Upgrades
{
    [CreateAssetMenu(menuName = "Data/Upgrade/Shield Capacity")]
    public class ShieldCapacity : Upgrade
    {
        public float capacityIncreasePercentage = 0.25f;

        public override void Apply(PlayerControllerComponent player)
        {
            base.Apply(player);

            ShipDefenseComponent shipDefense = player.GetComponent<ShipDefenseComponent>();
            shipDefense.maxShield += (shipDefense.maxShield * capacityIncreasePercentage);
        }
    }
}
