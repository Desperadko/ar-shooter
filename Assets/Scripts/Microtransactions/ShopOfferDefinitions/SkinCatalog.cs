using Game.Gameplay.Systems;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Shop
{
    [CreateAssetMenu(fileName = "SkinCatalog", menuName = "Scriptable Objects/SkinCatalog")]
    public class SkinCatalog : ScriptableObject
    {
        [SerializeField] private SkinDefinition[] skins;

        public IReadOnlyList<SkinDefinition> Skins => skins;

        public SkinDefinition GetSkinById(string id)
        {
            foreach (var skin in skins)
            {
                if (skin != null && skin.id == id)
                    return skin;
            }

            return null;
        }

        public SkinDefinition GetEquippedSkin(ElementalType elementalType, InventoryManager inventoryManager)
        {
            string equippedId = inventoryManager.GetEquippedItemId(elementalType);
            return GetSkinById(equippedId);
        }
    }
}
