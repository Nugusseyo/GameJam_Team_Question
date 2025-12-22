using Assets.JYG._Script;
using DG.Tweening;
using UnityEngine;

public class SinEnemy : Enemy
{
    protected override void EnemyMove()
    {
        _moveTween.Kill();
        _moveTween = DOTween.Sequence();
        Vector3 targetPos = (_target.position - transform.position).normalized;

    }
}
