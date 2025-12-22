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

        public void SpawnObstacle()
        {
            if (currentObstacles == null)
            {
                currentObstacles = new List<Obstacle>();
            }
            foreach (Obstacle obstacle in currentObstacles)
            {
                currentObstacles.Remove(obstacle);
                obstacle.Destroy();
            }

            for (int i = 0; i < spawnPoints.Length; i++)
            {
                currentObstacles.Add(Instantiate(obstaclePrefabs[UnityEngine.Random.Range(0, obstaclePrefabs.Length)], transform));
                currentObstacles[i].transform.position = spawnPoints[i].SpawnPoint;
                currentObstacles[i].SpawnObstacle(spawnPoints[i].NormalVector);
            }
        }
    }

    [Serializable]
    public struct spawnPoints
    {
        [field: SerializeField] public Vector2 NormalVector { get; private set; }
        [field: SerializeField] public Vector2 SpawnPoint {get; private set;}
    }
}
