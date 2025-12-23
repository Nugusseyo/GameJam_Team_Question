using DG.Tweening;
using UnityEngine;

public class ObjectShaker : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float shakeDuration = 0.5f;

    public void ShakeTarget()
    {
        target.transform.DOShakePosition(shakeDuration, new Vector2(1, 0), 10, 2f);
    }
}
