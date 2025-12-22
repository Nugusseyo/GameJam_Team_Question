using Assets.JYG._Script;
using DG.Tweening;
using UnityEngine;

public class NormalEnemy : Enemy
{
    protected override void EnemyMove()
    {
        _moveTween.Kill();
        _moveTween = DOTween.Sequence();
        Vector3 targetPos = (_target.position - transform.position).normalized;
        _moveTween.Append(transform.DOMove(targetPos * Distance + transform.position, Duration).SetDelay(MoveDelay).SetEase(Ease).OnComplete(EnemyMove));
    }
}
