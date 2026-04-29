using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Gameplay.Systems
{
    public class PlayerAttackSystem : MonoBehaviour
    {
        [SerializeField] private float damageMin;
        [SerializeField] private float damageMax;
        [SerializeField] private int critChance;
        [SerializeField] private float critMultiplier;

        public void Attack(GameObject enemy, ElementalType attackType)
        {
            if(enemy.TryGetComponent<EnemyHealthSystem>(out var health))
            {
                var damage = Random.Range(damageMin, damageMax);
                var isCrit = Random.Range(0, 100) < critChance;
                if (isCrit) damage *= critMultiplier;

                if(enemy.TryGetComponent<Elemental>(out var elemental))
                {
                    damage *= ElementSystem.GetMultiplier(attackType, elemental.Type);
                }

                health.TakeDamage((int)damage);
            }
        }
    }
}
