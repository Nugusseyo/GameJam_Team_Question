using JJW._02_Script.UI;
using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int shieldChargeTime = 3;
    [SerializeField] private IntEventChannel eventChannel;
    private int health;
    private bool shield = false;
    private int lastShield = 0;

    public bool hasShield = false;
    public event Action OnDead;
    public event Action OnDameged;

    private void Awake()
    {
        health = maxHealth;
        GameManager.Instance.Player.OnStop += Shield;
    }

    public void GetDamage(int amount)
    {
        if(shield)
        {
            lastShield = GameManager.Instance.TurnManager.Turn;
            shield = false;
            BreakShield();
            return;
        }
        health -= amount;
        eventChannel.Raise(health);
        health = Mathf.Clamp(health, 0, 10);
        if(health == 0)
        {
            OnDead?.Invoke();
        }
        else
        {
            OnDameged?.Invoke();
        }
    }

    public void GetHeal(int amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, 10);
        eventChannel.Raise(health);
    }

    public void GetAddictionalHealth()
    {
        maxHealth += 1;
        health += 1;
    }

    private void BreakShield()
    {
        
    }

    private void Shield()
    {
        if(!shield && hasShield)
        {
            if(lastShield+shieldChargeTime == GameManager.Instance.TurnManager.Turn)
            {
                shield = true;
            }
        }
    }
}
