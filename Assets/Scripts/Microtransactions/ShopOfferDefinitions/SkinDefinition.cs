using Game.Gameplay.Systems;
using UnityEngine;

namespace Game.Shop
{
    [CreateAssetMenu(fileName = "SkinItemDefinition", menuName = "Scriptable Objects/SkinItemDefinition")]
    public class SkinDefinition : ShopItemDefinition
    {
        public Material projectileMaterial;
        public Material trailMaterial;
        public ElementalType elementalType;
    }
}
