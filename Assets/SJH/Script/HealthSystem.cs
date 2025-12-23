using csiimnida.CSILib.SoundManager.RunTime;
using JJW._02_Script.UI;
using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int shieldChargeTime = 3;
    [SerializeField] private IntEventChannel eventChannel;
    [SerializeField] private ParticleSystem shieldParticle;
    [SerializeField] private ParticleSystem shieldParticle2;
    private int health;
    private bool shield = true;
    private int lastShield = 0;

    public bool hasShield = false;
    public event Action OnDead;
    public event Action OnDameged;

    private void Awake()
    {
        health = maxHealth;
    }

    private void Start()
    {
        eventChannel.Raise(health);
        GameManager.Instance.TurnManager.OnTurnPass.AddListener(Shield);
    }

    public void GetDamage(int amount)
    {
        if(shield && hasShield)
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
        SoundManager.Instance.PlaySound("P_Heal");
        health += amount;
        health = Mathf.Clamp(health, 0, maxHealth);
        eventChannel.Raise(health);
    }

    public void GetAddictionalHealth()
    {
        maxHealth += 1;
        health += 1;
        maxHealth = Mathf.Clamp(maxHealth, 0, 10);
        health = Mathf.Clamp(health, 0, maxHealth);
        eventChannel.Raise(health);
    }

    private void BreakShield()
    {
        SoundManager.Instance.PlaySound("P_P_ShieldBroken");
        shieldParticle.Play();
    }

    private void Shield()
    {
        if(!shield && hasShield)
        {
            if(lastShield+shieldChargeTime <= GameManager.Instance.TurnManager.Turn)
            {
                SoundManager.Instance.PlaySound("P_ShieldHeal");
                shieldParticle2.Play();
                shield = true;
            }
        }
    }
}
