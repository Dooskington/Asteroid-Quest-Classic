using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Data
{
    [CreateAssetMenu(menuName = "Data/Upgrade/Internal Gravity Field")]
    public class PickupRadius : Upgrade
    {
        public float rangeIncreasePercentage = 0.5f;

        public override void Apply(PlayerControllerComponent player)
        {
            base.Apply(player);

            ShipCargoComponent shipCargo = player.GetComponent<ShipCargoComponent>();
            shipCargo.pickupRadius += (shipCargo.pickupRadius * rangeIncreasePercentage);
        }
    }
}
