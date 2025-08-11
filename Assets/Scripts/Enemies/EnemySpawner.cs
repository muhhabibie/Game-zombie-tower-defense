using System;
using System.Collections;
using UnityEngine;

namespace LastBastion.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        public EnemyController enemyPrefab;
        public Transform[] spawnPoints;

        public int ActiveEnemyCount { get; private set; }

        public void SpawnWave(int count, float hpMultiplier, Action onFinishedSpawning)
        {
            StartCoroutine(SpawnRoutine(count, hpMultiplier, onFinishedSpawning));
        }

        private IEnumerator SpawnRoutine(int count, float hpMultiplier, Action onFinishedSpawning)
        {
            ActiveEnemyCount += count;
            for (int i = 0; i < count; i++)
            {
                Transform sp = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
                var enemy = Instantiate(enemyPrefab, sp.position, Quaternion.identity);
                enemy.Initialize(LastBastion.Core.GameManager.Instance.bastion, hpMultiplier);
                yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.35f));
            }
            onFinishedSpawning?.Invoke();
        }

        public void NotifyEnemyKilled(EnemyController e)
        {
            ActiveEnemyCount = Mathf.Max(0, ActiveEnemyCount - 1);
        }
    }
}