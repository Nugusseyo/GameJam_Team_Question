using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public int Acceleration { get; private set; } = 0;
    private Player player;
    private Damage damageC;
    private HealthSystem healthSystem;
    private List<UpgradeSO> myUpgrades = new List<UpgradeSO>();

    private int extraHealth = 0;

    private void Awake()
    {
        damageC = GetComponent<Damage>();
        player = GameManager.Instance.Player;
        healthSystem = GetComponent<HealthSystem>();
    }

    public void ChoiceUpgrade(UpgradeSO so)
    {
        myUpgrades.Add(so);
        switch(so.Type)
        {
            case UpgradeType.Health:
                healthSystem.GetAddictionalHealth();
                break;
            case UpgradeType.Damage:
                damageC.DamageIncrease += 1;
                break;
            case UpgradeType.Acceleration:
                Acceleration += 1;
                break;
            case UpgradeType.Speed:
                player.GetSpeed();
                break;
            case UpgradeType.Shield:
                healthSystem.hasShield = true;
                break;
            case UpgradeType.Heal:
                healthSystem.GetHeal(so.Amount);
                break;
            case UpgradeType.TheWorld:
                GameManager.Instance.Tooltip.TheWorld = true;
                break;
        }
    }
}
