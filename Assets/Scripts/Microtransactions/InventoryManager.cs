using Game.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Game.Shop
{
    public class InventoryManager : MonoBehaviour
    {
        public int GetItemLevel(string itemId)
        {
            var item = PlayerStateManager.Instance.CurrentState.ownedItems.Find(item => item.itemId == itemId);
            
            if (item == null)
            {
                return 0;
            }
            else
            {
                return item.level;
            }

        }

        public void AddOrUpdateItem(string itemId, int level)
        {

            var item = PlayerStateManager.Instance.CurrentState.ownedItems.Find(item => item.itemId == itemId);

            if(item == null)
            {
                PlayerStateManager.Instance.CurrentState.ownedItems.Add(new ItemProgress() { itemId = itemId, level = level });
            }
            else
            {
                item.level = level;
            }

            PlayerStateManager.Instance.Save();
        }
    }
}
