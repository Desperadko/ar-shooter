using Game.Gameplay.Systems;
using Game.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Game.Shop
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance;

        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

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

        public bool HasItem(string itemId)
        {
            return PlayerStateManager.Instance.CurrentState.ownedItems.Any(item => item.itemId == itemId);
        }

        public void EquipItem(string itemId, ElementalType elementalType)
        {
            switch (elementalType)
            {
                case ElementalType.Fire:
                    PlayerStateManager.Instance.CurrentState.equippedFireSkin = itemId;
                    break;
                case ElementalType.Water:
                    PlayerStateManager.Instance.CurrentState.equippedWaterSkin = itemId;
                    break;
                default:
                    PlayerStateManager.Instance.CurrentState.equippedNatureSkin = itemId;
                    break;
            }

            PlayerStateManager.Instance.Save();
        }

        public string GetEquippedItemId(ElementalType elementalType)
        {
            return elementalType switch
            {
                ElementalType.Fire => PlayerStateManager.Instance.CurrentState.equippedFireSkin,
                ElementalType.Water => PlayerStateManager.Instance.CurrentState.equippedWaterSkin,
                _ => PlayerStateManager.Instance.CurrentState.equippedNatureSkin
            };
        }
    }
}
