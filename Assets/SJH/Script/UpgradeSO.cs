using UnityEngine;

public enum UpgradeType
{
    Health,
    Damage,
    Acceleration,
    Shield,
    Speed,
    Heal,
    TheWorld
}

[CreateAssetMenu(fileName = "UpgradeSO", menuName = "SJH/SO/UpgradeSO")]
public class UpgradeSO : ScriptableObject
{
    public Sprite Image;
    public string Name;
    public UpgradeType Type;
    public int Amount;
    [TextArea]
    public string Desc;
}
