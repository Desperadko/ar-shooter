using Game.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Game.Gameplay.SpawnBehavior
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private ScanManager scanManager;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private float minimumSpawnDistance;
        [SerializeField] private List<PoolEntry> enemyEntries;
        [SerializeField] private float rate;

        private float lastSpawned = 0f;

        private void Awake()
        {
            GameManager.OnInitialized += StopSpawning;
            GameManager.OnGameStarted += StartSpawning;
            GameManager.OnGameResumed += StartSpawning;
            GameManager.OnGamePaused += StopSpawning;
            GameManager.OnGameOver += StopSpawning;
            GameManager.OnScan += StopSpawning;
        }

        private void OnDestroy()
        {
            GameManager.OnInitialized -= StopSpawning;
            GameManager.OnGameStarted -= StartSpawning;
            GameManager.OnGameResumed -= StartSpawning;
            GameManager.OnGamePaused -= StopSpawning;
            GameManager.OnGameOver -= StopSpawning;
            GameManager.OnScan -= StopSpawning;
        }

        private void StartSpawning() => enabled = true;
        private void StopSpawning() => enabled = false;

        private void Update()
        {
            if(Time.time > lastSpawned + rate)
            {
                SpawnRandomEnemy();
                lastSpawned = Time.time;
            }
        }

        private void SpawnRandomEnemy()
        {
            if (enemyEntries.Count <= 0) return;

            var index = Random.Range(0, enemyEntries.Count);
            SpawnEnemy(enemyEntries[index]);
        }

        private void SpawnEnemy(PoolEntry poolable)
        {
            if (scanManager.MainPlane == null) return;

            var position = GetRandomSpawnPointAwayFromPlayer(scanManager.MainPlane);
            Vector3 direction = (playerTransform.position - position).normalized;
            direction.y = 0f;

            ObjectPoolManager.Spawn(poolable, position, Quaternion.LookRotation(direction));
        }

        private Vector3 GetRandomSpawnPointAwayFromPlayer(ARPlane plane)
        {
            for(int attempt = 0; attempt < 10; attempt++)
            {
                var point = GetRandomEdgePoint(plane);

                if(Vector3.Distance(point, playerTransform.position) >= minimumSpawnDistance)
                {
                    return point;
                }
            }

            return GetRandomEdgePoint(plane);
        }

        private Vector3 GetRandomEdgePoint(ARPlane plane)
        {
            var boundary = plane.boundary;

            var index = Random.Range(0, boundary.Length);
            var nextIndex = (index + 1) % boundary.Length;

            var t = Random.Range(0f, 1f);
            var localPoint = Vector2.Lerp(boundary[index], boundary[nextIndex], t);

            return plane.transform.TransformPoint(new Vector3(localPoint.x, 0, localPoint.y));
        }
    }
}
