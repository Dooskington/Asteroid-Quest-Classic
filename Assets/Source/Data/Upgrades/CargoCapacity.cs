using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Data.Upgrades
{
    [CreateAssetMenu(menuName = "Data/Upgrade/Cargo Capacity")]
    public class CargoCapacity : Upgrade
    {
        public float capacityIncreasePercentage = 0.25f;

        public override void Apply(PlayerControllerComponent player)
        {
            base.Apply(player);

            ShipCargoComponent shipCargo = player.GetComponent<ShipCargoComponent>();
            shipCargo.cargoHoldSize += (int) (shipCargo.cargoHoldSize * capacityIncreasePercentage);
        }
    }
}
