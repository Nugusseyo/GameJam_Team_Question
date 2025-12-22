using Assets.JYG._Script;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region Singleton
    public static EnemyManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public List<Enemy> enemyList = new List<Enemy>();
    public List<GameObject> spawnEnemyList = new List<GameObject>();
    public List<Vector2> enemySpawnPoint = new List<Vector2>();

    [SerializeField] private float cameraHeight;
    [SerializeField] private float cameraWidth;
    [SerializeField] private float distance;

    private void Start()
    {
        InitSpawnPosition();
    }

    

    public void AddEnemy(Enemy enemy)
    {
        if (enemy == null) return;
        enemyList.Add(enemy);
    }
    public void RemoveEnemy(Enemy enemy)
    {
        if (enemy == null) return;
        enemyList.Remove(enemy);
    }

    public void ResetEnemy()
    {
        if(enemyList.Count == 0) return;
        foreach (Enemy enemy in enemyList)
        {
            enemy.CurrentHealth = enemy.MaxHealth;
        }
    }
    [ContextMenu("Stop Enemy")]
    public void StopEnemy()
    {
        foreach(Enemy enemy in enemyList)
        {
            enemy.StopEnemy();
        }
    }

    [ContextMenu("Start Move Enemy")]
    public void StartMoveEnemy()
    {
        foreach(Enemy enemy in enemyList)
        {
            enemy.StartMoveEnemy();
        }
    }
    [ContextMenu("Enemy Attacked")]
    public void MinusEnemyHealth(int value)
    {
        foreach(Enemy enemy in enemyList)
        {
            enemy.CurrentHealth += value;
        }
    }

    [ContextMenu("Spawn Enemy Random Position")]
    public void SpawnEnemyRandomPosition()
    {
        foreach(Vector2 pos in enemySpawnPoint)
        {
            Instantiate(spawnEnemyList[UnityEngine.Random.Range(0, spawnEnemyList.Count)], pos, Quaternion.identity);
        }
    }
    private void InitSpawnPosition()
    {
        Vector2 cameraMaxSize = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        cameraHeight = cameraMaxSize.y;
        cameraWidth = cameraMaxSize.x;

        enemySpawnPoint.Add(new Vector2(0, cameraHeight + distance));
        enemySpawnPoint.Add(new Vector2(0 , - cameraHeight - distance));
        enemySpawnPoint.Add(new Vector2(cameraWidth + distance, 0));
        enemySpawnPoint.Add(new Vector2(-cameraWidth - distance, 0));
        enemySpawnPoint.Add(new Vector2(-cameraWidth - distance, cameraHeight + distance));
        enemySpawnPoint.Add(new Vector2(-cameraWidth - distance, -cameraHeight - distance));
        enemySpawnPoint.Add(new Vector2(cameraWidth + distance, -cameraHeight - distance));
        enemySpawnPoint.Add(new Vector2(cameraWidth + distance, cameraHeight + distance));
    }
}
