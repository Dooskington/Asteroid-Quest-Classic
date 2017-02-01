using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Data.Upgrades
{
    [CreateAssetMenu(menuName = "Data/Upgrade/Sell Prices")]
    public class SellPrices : Upgrade
    {
        public float sellPriceIncrease = 0.0f;

        public override void Apply(PlayerControllerComponent player)
        {
            base.Apply(player);

            player.sellPriceModifier += player.sellPriceModifier * sellPriceIncrease;
        }
    }
}
