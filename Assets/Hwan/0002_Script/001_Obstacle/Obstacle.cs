using DG.Tweening;
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
                punch: Vector3.one * 0.5f, // ¾ó¸¶³ª Ä¿ÁúÁö
                duration: 0.175f,            // ÀüÃ¼ ½Ã°£
                vibrato: 1,                // Èçµé¸² È½¼ö
                elasticity: 0.8f           // Æ¨±è Á¤µµ
            ).OnComplete(() => col.enabled = true);
        }

        protected abstract void Initialize();
        public abstract void OnPlayerReached();
        public virtual void Destroy()
        {
            if (IsDestroyed) return;
            IsDestroyed = true;
            spriteRen.enabled = false;
            deadParticle.Play();
            transform.DOKill();
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