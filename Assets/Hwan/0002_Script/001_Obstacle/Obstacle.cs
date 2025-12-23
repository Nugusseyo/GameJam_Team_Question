using csiimnida.CSILib.SoundManager.RunTime;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Hwan
{
    public abstract class Obstacle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private ParticleSystem deadParticle;
        private SpriteRenderer spriteRen;
        public bool IsDestroyed { get; private set; }
        [SerializeField] private ObstacleSO obstacleSO;
        protected Vector2 normalVector;
        private Collider2D col;
        public event Action OnDestroy;
        public Color Color => spriteRen.color;

        public virtual void SpawnObstacle(Vector2 normalVector)
        {
            this.normalVector = normalVector;

            col = GetComponent<Collider2D>();

            deadParticle = transform.GetChild(0).GetComponent<ParticleSystem>();

            spriteRen = GetComponentInChildren<SpriteRenderer>();
            Color useColor = spriteRen.color;

            var main = deadParticle.main;
            main.startColor = useColor;

            PointMove();

            Initialize();
        }

        private void PointMove()
        {
            col.enabled = false;
            transform.DOKill();
            transform.localScale = Vector3.one;

            transform.DOPunchScale(
                punch: Vector3.one * 0.5f, // 얼마나 커질지
                duration: 0.175f,            // 전체 시간
                vibrato: 1,                // 흔들림 횟수
                elasticity: 0.8f           // 튕김 정도
            ).OnComplete(() => col.enabled = true);
        }

        protected abstract void Initialize();
        public virtual void OnPlayerReached()
        {
            SoundManager.Instance.PlaySound("O_Reach");
        }
        public virtual void Destroy()
        {
            if (IsDestroyed) return;
            IsDestroyed = true;
            spriteRen.enabled = false;
            deadParticle.Play();
            transform.DOKill();
            OnDestroy?.Invoke();
            Destroy(gameObject, deadParticle.main.duration);
        }

        public virtual string GetObstacleDesc()
        {
            return obstacleSO.Desc;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            GameManager.Instance.Tooltip.ChangeText(GetObstacleDesc());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            GameManager.Instance.Tooltip.ChangeText("");
        }
    }
}