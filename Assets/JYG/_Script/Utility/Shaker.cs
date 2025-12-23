using DG.Tweening;
using UnityEngine;

public class ObjectShaker : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float shakeDuration = 0.5f;

    public void ShakeTarget()
    {
        target.transform.DOShakePosition(shakeDuration, new Vector2(Random.Range(-0.2f, 0.2f), 0), 6, 2f, true);
    }
}
