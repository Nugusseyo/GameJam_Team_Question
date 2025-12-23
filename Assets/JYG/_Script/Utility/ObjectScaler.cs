using DG.Tweening;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ObjectScaler : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private float scale = 2f;

    public void ShakeTarget()
    {
        target.transform.DOScale(new Vector3(2, 2, 0), duration);
    }
}
