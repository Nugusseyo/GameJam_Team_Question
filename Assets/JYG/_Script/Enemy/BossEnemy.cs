using Assets.JYG._Script;
using DG.Tweening;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    Enemy enemy;
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DeadEnemy();
    }

    private void DeadEnemy()
    {
        if(enemy.CurrentHealth > 0)
        {
            GameManager.Instance.Player.HealthSystem.GetDamage(100);
        }
    }
}
