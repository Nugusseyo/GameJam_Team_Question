using DG.Tweening;
using System.Linq;
using UnityEngine;

public class DOTweenFadeOut : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float duration = 0.5f;

    public void DoFade()
    {
        target.GetComponentsInChildren<SpriteRenderer>().ToList().ForEach(spriteRenderer =>
        {
            spriteRenderer.DOFade(0f, duration);
        });
    }
}
