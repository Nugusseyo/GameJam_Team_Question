namespace Hwan
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private Obstacle[] obstaclePrefabs;
        [SerializeField] private List<Obstacle> currentObstacles;
        [SerializeField] private spawnPoints[] spawnPoints;
        private float[] obstacleWeights;

        public void Awake()
        {
            currentObstacles = new List<Obstacle>();

            obstacleWeights = new float[obstaclePrefabs.Length];

            for (int i = 0; i < obstacleWeights.Length; i++)
                obstacleWeights[i] = 1f; // 기본 확률
        }

        public void SpawnObstacle()
        {
            foreach (Obstacle obstacle in currentObstacles)
            {
                currentObstacles.Remove(obstacle);
                obstacle.Destroy();
            }

            for (int i = 0; i < spawnPoints.Length; i++)
            {
                int index = GetRandomObstacleIndex();

                // 생성
                currentObstacles.Add(
                    Instantiate(obstaclePrefabs[index], transform)
                );

                currentObstacles[i].transform.position = spawnPoints[i].SpawnPoint;
                currentObstacles[i].SpawnObstacle(spawnPoints[i].NormalVector);
                
                obstacleWeights[index] *= 0.5f;

            }
        }

        int GetRandomObstacleIndex()
        {
            float totalWeight = 0f;

            for (int i = 0; i < obstacleWeights.Length; i++)
                totalWeight += obstacleWeights[i];

            float rand = UnityEngine.Random.Range(0f, totalWeight);

            float current = 0f;
            for (int i = 0; i < obstacleWeights.Length; i++)
            {
                current += obstacleWeights[i];
                if (rand <= current)
                    return i;
            }

            return 0;
        }
    }

    [Serializable]
    public struct spawnPoints
    {
        [field: SerializeField] public Vector2 NormalVector { get; private set; }
        [field: SerializeField] public Vector2 SpawnPoint {get; private set;}
    }
}
