using UnityEngine;
using System.Collections;

namespace Game.Gameplay.Systems
{
	public class ElementSystem: MonoBehaviour
	{
		public static float GetMultiplier(ElementalType projectile, ElementalType enemy)
		{
            // Strong against
            if (projectile == ElementalType.Fire && enemy == ElementalType.Nature) return 2f;
            if (projectile == ElementalType.Nature && enemy == ElementalType.Water) return 2f;
            if (projectile == ElementalType.Water && enemy == ElementalType.Fire) return 2f;

            // Weak against
            if (projectile == ElementalType.Fire && enemy == ElementalType.Water) return 0.5f;
            if (projectile == ElementalType.Nature && enemy == ElementalType.Fire) return 0.5f;
            if (projectile == ElementalType.Water && enemy == ElementalType.Nature) return 0.5f;

            // Neutral
            return 1f;
        }
	}

	public enum ElementalType
	{
		None, Water, Fire, Nature
	}
}