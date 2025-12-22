using UnityEngine;

public enum UpgradeType
{
    Health,
    Damage,
    Acceleration,
    Shield,
    Speed,
    Heal
}

[CreateAssetMenu(fileName = "UpgradeSO", menuName = "SJH/SO/UpgradeSO")]
public class UpgradeSO : ScriptableObject
{
    public string Name;
    public UpgradeType Type;
    public int Amount;
    [TextArea]
    public string Desc;
}
