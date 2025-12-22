using Assets.JYG._Script;
using DG.Tweening;
using UnityEngine;

public class RabbitStepEnemy : Enemy
{
    int _rotationAngle = 0;
    [SerializeField] private int hopCount = 3;
    [SerializeField] private int rotationPower = 45;
    protected override void EnemyMove()
    {
        _moveTween.Kill();
        _moveTween = DOTween.Sequence();
        Vector3 targetPos = (_target.position - transform.position).normalized;
        Vector3 finPos = targetPos * Distance;
        Vector3 prevTransform = transform.position;
        for (int i = 0; i < hopCount; i++)
        {
            _moveTween.Append(transform.DOMove(finPos / hopCount * (i + 1) + prevTransform, Duration / hopCount).SetEase(Ease));
            _moveTween.Join(transform.DORotate(new Vector3(transform.rotation.x, transform.rotation.y, _rotationAngle += rotationPower), Duration / hopCount));
        }
        _moveTween.SetDelay(MoveDelay);
        _moveTween.OnComplete(EnemyMove);
        //_moveTween.Play();
    }
}
