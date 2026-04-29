using Game.UI;
using UnityEngine;

namespace Game.Gameplay.Systems
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyMovementSystem : MonoBehaviour
    {
        public float Speed { get => speed; private set => speed = value; }
        [SerializeField] private float speed;

        private float currentSpeed;

        public void Move(Vector3 direction)
        {
            transform.position += currentSpeed * Time.deltaTime * direction;
        }
        
        public void StartMoving()
        {
            currentSpeed = Speed;
        }

        public void StopMoving()
        {
            currentSpeed = 0f;
        }

        public void UpdateSpeed(float speed)
        {
            Speed = speed;
            currentSpeed = speed;
        }
    }
}