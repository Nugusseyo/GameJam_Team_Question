using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeListSO", menuName = "SJH/SO/UpgradeListSO")]
public class UpgradeListSO : ScriptableObject
{
    public List<UpgradeSO> Upgrades;
}
