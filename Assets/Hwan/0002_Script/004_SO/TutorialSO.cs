namespace Hwan
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "TutorialSO", menuName = "HwanSO/TutorialSO")]
    public class TutorialSO : ScriptableObject
    {
        [field: SerializeField] public string Text { get; private set; }
        [field: SerializeField] public Vector2 Position { get; private set; }
        [field: SerializeField] public TutorialType TutoType { get; private set; }
        [field: SerializeField] public float WaitTime { get; private set; }
    }

}