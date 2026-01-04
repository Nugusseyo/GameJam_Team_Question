using Assets.JYG._Script;
using csiimnida.CSILib.SoundManager.RunTime;
using Hwan;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region Singleton
    public static EnemyManager Instance;

    private void Awake()
    {
        InitSpawnPosition();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        GameManager.Instance.TurnManager.OnTurnPass.AddListener(OnTurnEnd);
        GameManager.Instance.TurnManager.OnRoundComplete.AddListener((turn) =>
        {
            if (spawnCountSO.BossTurn == turn)
            {
                Debug.Log("Boss Spawned");
                prevSpawnPos = new List<Vector2>(enemySpawnPoint);
                Vector2 spawnPos = prevSpawnPos[UnityEngine.Random.Range(0, prevSpawnPos.Count)];
                Enemy enemy = Instantiate(boss, spawnPos, Quaternion.identity).GetComponentInChildren<Enemy>();
                prevSpawnPos.Remove(spawnPos);
                EnemyHealthShowcase health = Instantiate(healthCount, spawnPos, Quaternion.identity).GetComponent<EnemyHealthShowcase>();
                enemy.healthShowcase = health;
                health.Initialize(enemy);
                return;
            }
            int spawnCount = 0;
            foreach (SpawnCount standard in spawnCountSO.SpawnCounts)
            {
                if (turn/GameManager.Instance.TurnManager.completeTurnCount + 1 >= standard.Round)
                {
                    spawnCount = standard.EnemyCount;
                }
            }

            prevSpawnPos = new List<Vector2>(enemySpawnPoint);
            for (int i = 0; i < spawnCount; i++)
            {
                SpawnEnemyRandomPosition();
            }
        });
    }
    #endregion

    public GameObject boss;
    public GameObject healthCount;
    public List<Enemy> enemyList = new List<Enemy>();
    public List<GameObject> spawnEnemyList = new List<GameObject>();
    public List<Vector2> enemySpawnPoint = new List<Vector2>();

    private List<Vector2> prevSpawnPos = new List<Vector2>();

    [SerializeField] private float cameraHeight;
    [SerializeField] private float cameraWidth;
    [SerializeField] private float distance;
    [SerializeField] private SpawnCountSO spawnCountSO;
    public int ResetSkipTurn { get; set; } = 0;
    public int MoveSkipTurn { get; set; } = 0;

    private void OnTurnEnd()
    {
        if (MoveSkipTurn > 0)
        {
            MoveSkipTurn--;
        }
        else
        {
            StartMoveEnemy();
        }
        if (ResetSkipTurn > 0)
        {
            ResetSkipTurn--;
        }
        else
        {
            ResetEnemy();
        }
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
        if (TutorialManager.Instance.DoTuto == true) return;
        if (enemyList.Count == 0)
        {
            GameManager.Instance.TurnManager.SkipRound();
        }
    }

    public void ResetEnemy()
    {
        if(enemyList.Count == 0) return;
        SoundManager.Instance.PlaySound("E_Heal");
        foreach (Enemy enemy in enemyList)
        {
            enemy.CurrentHealth = enemy.MaxHealth;
        }
    }
    public void EnemyAllDead()
    {
        if (enemyList.Count == 0) return;
        SoundManager.Instance.PlaySound("E_Explosion");
        foreach (Enemy enemy in enemyList)
        {
            enemy.DeadEnemy();
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
    public void PlusEnemyHealth(int value)
    {
        bool isShutdown = false;
        foreach(Enemy enemy in enemyList)
        {
            enemy.CurrentHealth += value;
            if(enemy.CurrentHealth <= 0)
            {
                isShutdown = true;
            }
        }
        if(isShutdown)
        {
            SoundManager.Instance.PlaySound("E_Shutdown");
        }
        if(value > 0)
        {
            SoundManager.Instance.PlaySound("E_Heal");
        }
        else if(value < 0)
        {
            SoundManager.Instance.PlaySound("E_Explosion");
        }
    }

    [ContextMenu("Spawn Enemy Random Position")]
    public void SpawnEnemyRandomPosition()
    {
        if(prevSpawnPos.Count == 0)
        {
            prevSpawnPos = new List<Vector2>(enemySpawnPoint);
            Debug.LogWarning("@@@@@@@@@@ Spawn Position is Full. Checking SO Plz!!!! @@@@@@@@@@");
        }
        Vector2 spawnPos = prevSpawnPos[UnityEngine.Random.Range(0, prevSpawnPos.Count)];
        Enemy enemy = null;
        do
        {
            enemy = Instantiate(spawnEnemyList[UnityEngine.Random.Range(0, spawnEnemyList.Count)], spawnPos, Quaternion.identity).GetComponentInChildren<Enemy>();
            prevSpawnPos.Remove(spawnPos);
            EnemyHealthShowcase health = Instantiate(healthCount, spawnPos, Quaternion.identity).GetComponent<EnemyHealthShowcase>();
            enemy.healthShowcase = health;
            health.Initialize(enemy);
        }
        while (TutorialManager.Instance.DoTuto == true && enemy.CurrentHealth == 3);
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

    private void Start()
    {
        GameManager.Instance.Player.OnBump += HandlePlayerBump;
    }


    private void OnDisable()
    {
        GameManager.Instance.Player.OnBump -= HandlePlayerBump;
    }

    private void HandlePlayerBump()
    {
        PlusEnemyHealth(-GameManager.Instance.Player.DamageC.DamageIncrease);
    }
}
