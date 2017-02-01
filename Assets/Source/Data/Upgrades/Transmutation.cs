using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Data.Upgrades
{
    [CreateAssetMenu(menuName = "Data/Upgrade/Transmutation")]
    public class Transmutation : Upgrade
    {
        public float transmuteFrequencyIncrease = 0.0f;
        public float transmuteUsageDecrease = 0.0f;

        public override void Apply(PlayerControllerComponent player)
        {
            base.Apply(player);

            ShipCargoComponent shipCargo = player.GetComponent<ShipCargoComponent>();
            shipCargo.isTransmutationEnabled = true;
            shipCargo.transmuteFrequency -= (shipCargo.transmuteFrequency * transmuteFrequencyIncrease);
            shipCargo.transmutePowerUsage -= (shipCargo.transmutePowerUsage * transmuteUsageDecrease);
        }
    }
}
