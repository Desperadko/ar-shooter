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
    }

    [Serializable]
    public class ItemProgress
    {
        public string itemId;
        public int level;
    }
}
