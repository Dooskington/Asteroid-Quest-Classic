using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Data
{
    [CreateAssetMenu(menuName = "Data/Upgrade/Laser Damage")]
    public class LaserDamage : Upgrade
    {
        public float damageIncreasePercentage = 0.5f;

        public override void Apply(PlayerControllerComponent player)
        {
            base.Apply(player);

            ShipWeaponComponent shipWeapon = player.GetComponent<ShipWeaponComponent>();
            shipWeapon.damage += (shipWeapon.damage * damageIncreasePercentage);
        }
    }
}
