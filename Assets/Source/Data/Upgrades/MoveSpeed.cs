using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Data.Upgrades
{
    [CreateAssetMenu(menuName = "Data/Upgrade/Move Speed")]
    public class MoveSpeed : Upgrade
    {
        public float increasePercentage = 0.25f;

        public override void Apply(PlayerControllerComponent player)
        {
            base.Apply(player);

            ShipMovementComponent shipMovement = player.GetComponent<ShipMovementComponent>();
            shipMovement.moveSpeed += (shipMovement.moveSpeed * increasePercentage);
            shipMovement.maxSpeed += (shipMovement.maxSpeed * increasePercentage);
        }
    }
}
