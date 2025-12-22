using Assets.JYG._Script;
using DG.Tweening;
using UnityEngine;

public class NormalEnemy : Enemy
{
    private Rotation _rotation;

    protected override void Awake()
    {
        base.Awake();
        _rotation = GetComponent<Rotation>();
    }
    protected override void EnemyMove()
    {
        _moveTween.Kill();
        _moveTween = DOTween.Sequence();
        Vector3 targetPos = (_target.position - transform.position).normalized;
        _moveTween.Append(transform.DOMove(targetPos * Distance + transform.position, Duration).SetDelay(MoveDelay).SetEase(Ease).OnComplete(EnemyMove));
    }

    public override void StopEnemy()
    {
        base.StopEnemy();
        _rotation.StopRotation();
    }
    public override void StartMoveEnemy()
    {
        base.StartMoveEnemy();
        _rotation.RotationSelf();
    }
}
