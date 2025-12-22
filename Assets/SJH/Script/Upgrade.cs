using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public int Acceleration { get; private set; } = 0;
    private Player player = GameManager.Instance.Player;
    private Damage damageC;
    private List<UpgradeSO> myUpgrades = new List<UpgradeSO>();

    private int extraHealth = 0;

    private void Awake()
    {
        damageC = GetComponent<Damage>();
    }

    public void ChoiceUpgrade(UpgradeSO so)
    {
        myUpgrades.Add(so);
        switch(so.Type)
        {
            case UpgradeType.Health:
                player.HealthSystem.GetAddictionalHealth();
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
                player.HealthSystem.hasShield = true;
                break;
            case UpgradeType.Heal:
                player.HealthSystem.GetHeal(so.Amount);
                break;
        }
    }
}
