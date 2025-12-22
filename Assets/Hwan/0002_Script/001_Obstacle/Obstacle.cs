using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace Hwan
{
    public abstract class Obstacle : MonoBehaviour
    {
        [SerializeField] private ParticleSystem deadParticle;
        private SpriteRenderer spriteRen;
        public bool IsDestroyed { get; private set; }
        [SerializeField] private ObstacleSO obstacleSO;
        protected Vector2 normalVector;

        public void SpawnObstacle(Vector2 normalVector)
        {
            this.normalVector = normalVector;
            spriteRen = GetComponent<SpriteRenderer>();
            spriteRen.color = obstacleSO.Color;
            //deadParticle.main.startColor = obstacleSO.Color;

            PointMove();

            Initialize();
        }

        private void PointMove()
        {
            transform.DOKill();
            transform.localScale = Vector3.one;

            transform.DOPunchScale(
                punch: Vector3.one * 0.2f, // 얼마나 커질지
                duration: 0.2f,            // 전체 시간
                vibrato: 1,                // 흔들림 횟수
                elasticity: 0.8f           // 튕김 정도
            );
        }

        protected abstract void Initialize();
        public abstract void OnPlayerReached();
        public virtual void Destroy()
        {
            transform
                .DOScale(Vector3.one * 1.2f, 0.1f)   // 순간적으로 커짐
                .SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    transform
                        .DOScale(Vector3.zero, 0.15f) // 그대로 0으로 줄어들며 사라짐
                        .SetEase(Ease.InBack)
                        .OnComplete(() =>
                        {
                            IsDestroyed = true;
                            spriteRen.enabled = false;
                            deadParticle.Play();
                            Destroy(gameObject, deadParticle.main.duration);
                            transform.DOKill();
                        });
                });
        }

        public string GetObstacleDesc()
        {
            return obstacleSO.Desc;
        }
    }
}