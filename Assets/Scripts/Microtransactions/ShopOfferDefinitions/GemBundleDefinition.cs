using UnityEngine;

namespace Game.Shop
{
    [CreateAssetMenu(fileName = "GemBundleDefinition", menuName = "Scriptable Objects/GemBundleDefinition")]
    public class GemBundleDefinition : ScriptableObject
    {
        public string id;
        public string displayName;
        public Sprite icon;
        public float simulatedPrice;
        public int baseAmount;
        public int bonusAmount;
        public int TotalAmount { get => baseAmount + bonusAmount; }
        public bool bestPrice;
    }
}
