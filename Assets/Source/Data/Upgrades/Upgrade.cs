using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Data
{
    public class Upgrade : ScriptableObject
    {
        public string upgradeName = "Upgrade";
        public Sprite sprite;
        public int cost = 250;
        public List<Upgrade> requiredUpgrades = new List<Upgrade>();

        public virtual void Apply(PlayerControllerComponent player)
        {
            Debug.Log(upgradeName + " applied.");

            player.Upgrades.Add(this);
        }
    }
}
