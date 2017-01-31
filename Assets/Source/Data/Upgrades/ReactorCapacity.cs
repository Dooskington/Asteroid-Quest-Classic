using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Data.Upgrades
{
    [CreateAssetMenu(menuName = "Data/Upgrade/Reactor Capacity")]
    public class ReactorCapacity : Upgrade
    {
        public float capacityIncreasePercentage = 0.25f;

        public override void Apply(PlayerControllerComponent player)
        {
            base.Apply(player);

            ShipReactorComponent shipReactor = player.GetComponent<ShipReactorComponent>();
            shipReactor.maxCoreHealth += shipReactor.maxCoreHealth * capacityIncreasePercentage;
            shipReactor.coreHealth = shipReactor.maxCoreHealth;
        }
    }
}
