using DG.Tweening;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    Sequence rotation;
    public void RotationSelf()
    {
        rotation = DOTween.Sequence();
        rotation.Append(transform.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart));
    }
    public void StopRotation()
    {
        rotation.Kill();
    }
}
