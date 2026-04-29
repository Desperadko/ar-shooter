using Game.UI;
using UnityEngine;

namespace Game.Gameplay.Systems
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyMovementSystem : MonoBehaviour
    {
        public float Speed { get => speed; private set => speed = value; }
        [SerializeField] private float speed;
        
        [SerializeField] private Rigidbody rigidBody;

        private float currentSpeed;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
        }

        public void Move(Vector3 direction)
        {
            rigidBody.MovePosition(transform.position + (currentSpeed * Time.deltaTime * direction));

            //use while testing
            //transform.position += direction * speed * Time.deltaTime;
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