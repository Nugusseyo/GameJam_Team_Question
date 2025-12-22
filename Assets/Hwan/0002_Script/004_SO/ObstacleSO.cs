namespace Hwan
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "ObstacleSO", menuName = "HwanSO/ObstacleSO")]
    public class ObstacleSO : ScriptableObject
    {
        [field: SerializeField] public Vector2 Size { get; private set; }
        [field: SerializeField] public Color Color { get; private set; }
        [field: SerializeField] public string Desc { get; private set; }
    }
}
