using DG.Tweening;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ObjectShaker : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float duration = 1f;

    public void ShakeTarget()
    {
        target.transform.DOShakePosition(duration);
    }
}
