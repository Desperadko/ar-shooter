using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Persistence
{
    [Serializable]
    public class PlayerState
    {
        public int currency = 100;
        public List<ItemProgress> ownedItems = new List<ItemProgress>();
        public string equippedFireSkin;
        public string equippedWaterSkin;
        public string equippedNatureSkin;

        public PlayerState()
        {
            currency = 100;

            ownedItems = new List<ItemProgress>
            {
                new() { itemId = "fire_skin", level = 1 },
                new() { itemId = "water_skin", level = 1},
                new() { itemId = "nature_skin", level = 1}
            };

            equippedFireSkin = "fire_skin";
            equippedWaterSkin = "water_skin";
            equippedNatureSkin = "nature_skin";
        }
    }

    [Serializable]
    public class ItemProgress
    {
        public string itemId;
        public int level;
    }
}
