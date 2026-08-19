using UnityEngine;

namespace Game.Shop
{
    [CreateAssetMenu(fileName = "ShopItemDefinition", menuName = "Scriptable Objects/ShopItemDefinition")]
    public abstract class ShopItemDefinition : ScriptableObject
    {
        public string id;
        public string displayName;
        public string description;
        public Sprite icon;
        public int[] prices;
        public ShopCategory category;

        public int MaxPurchases => prices.Length;

        public int GetPrice(int level)
        {
            if (level < 0 || level >= MaxPurchases)
                return -1;

            return prices[level];
        }
    }

    public enum ShopCategory
    {
        None, Skin, Convenience, Upgrade
    }
}