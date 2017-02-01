using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Data.Upgrades
{
    [CreateAssetMenu(menuName = "Data/Upgrade/Reactor Effeciency")]
    public class ReactorEfficiency : Upgrade
    {
        public float increasePercentage = 0.25f;

        public override void Apply(PlayerControllerComponent player)
        {
            base.Apply(player);

            ShipReactorComponent shipReactor = player.GetComponent<ShipReactorComponent>();
            shipReactor.usageModifier *= (1.0f - increasePercentage);
        }
    }
}
