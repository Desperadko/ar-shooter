using UnityEngine;

namespace Game.Gameplay.Systems
{
    public class LookingSystem : MonoBehaviour
    {
        [HideInInspector] public Vector3 LookingDirectionNormalized { get => lookingDirectionNormalized; }

        private Transform target;
        private Vector3 lookingDirectionNormalized;

        private void Awake()
        {
            target = Camera.main.transform;
            lookingDirectionNormalized = transform.forward;
        }

        private void Update()
        {
            if(target is not null)
            {
                var direction = LookAtTarget(target.position);
                lookingDirectionNormalized = direction;
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }

        private Vector3 LookAtTarget(Vector3 target)
        {
            var direction = target - transform.position;
            direction.y = 0f;

            return direction.normalized;
        }
    }
}
