using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnCountSO", menuName = "HwanSO/SpawnCountSO")]
public class SpawnCountSO : ScriptableObject
{
    [field: SerializeField] public SpawnCount[] SpawnCounts { get; private set; }
}

[Serializable]
public struct SpawnCount
{
    [field: SerializeField] public int EnemyCount;
    [field: SerializeField] public int Round;
}
