namespace Hwan
{
    using csiimnida.CSILib.SoundManager.RunTime;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] obstaclePrefabs;
        private List<Obstacle> currentObstacles;
        [SerializeField] private spawnPoints[] spawnPoints;
        private float[] obstacleWeights;
        private Dictionary<NormalVectorType, Vector2> normalVectorDictionary = new();

        public void Awake()
        {
            normalVectorDictionary.Add(NormalVectorType.Up, Vector2.up);
            normalVectorDictionary.Add(NormalVectorType.Down, Vector2.down);
            normalVectorDictionary.Add(NormalVectorType.Left, Vector2.left);
            normalVectorDictionary.Add(NormalVectorType.Right, Vector2.right);

            currentObstacles = new List<Obstacle>();

            obstacleWeights = new float[obstaclePrefabs.Length];

            for (int i = 0; i < obstacleWeights.Length; i++)
                obstacleWeights[i] = 1f; // �⺻ Ȯ��

            GameManager.Instance.TurnManager.OnRoundComplete.AddListener(SpawnObstacle);
        }

        public void SpawnObstacle(int _)
        {
            SoundManager.Instance.PlaySound("O_Change");
            for (int i = currentObstacles.Count - 1; i >= 0; i--)
            {
                var obs = currentObstacles[i];
                currentObstacles.RemoveAt(i);
                obs.Destroy();
            }

            for (int i = 0; i < spawnPoints.Length; i++)
            {
                int index = GetRandomObstacleIndex();

                // ����
                currentObstacles.Add(Instantiate(obstaclePrefabs[index], transform).GetComponent<Obstacle>());
                currentObstacles[i].transform.position = spawnPoints[i].SpawnPoint.position;
                currentObstacles[i].SpawnObstacle(normalVectorDictionary[spawnPoints[i].NormalVector]);
                
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
        [field: SerializeField] public NormalVectorType NormalVector { get; private set; }
        [field: SerializeField] public Transform SpawnPoint {get; private set;}
    }
}
