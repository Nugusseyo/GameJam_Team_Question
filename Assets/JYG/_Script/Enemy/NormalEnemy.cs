using Assets.JYG._Script;
using DG.Tweening;
using UnityEngine;

public class NormalEnemy : Enemy
{
    protected override void Start()
    {
        base.Start();
        transform.DORotate(new Vector3(0, 0, 360), 3f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
    }
    protected override void EnemyMove()
    {
        _moveTween.Kill();
        _moveTween = DOTween.Sequence();
        Vector3 targetPos = (_target.position - transform.position).normalized;
        _moveTween.Append(transform.DOMove(targetPos * Distance + transform.position, Duration).SetDelay(MoveDelay).SetEase(Ease));
        _moveTween.OnComplete(EnemyMove);
    }

    public override void StopEnemy()
    {
        base.StopEnemy();
        transform.DOPause();
    }
    public override void StartMoveEnemy()
    {
        base.StartMoveEnemy();
        transform.DOPlay();
    }
}
