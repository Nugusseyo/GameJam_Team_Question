using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace Hwan
{
    public abstract class Obstacle : MonoBehaviour
    {
        [SerializeField] private ParticleSystem deadParticle;
        [SerializeField] private SpriteRenderer spriteRen;
        public bool IsDestroyed { get; private set; }
        [SerializeField] private ObstacleSO obstacleSO;

        public void SpawnObstacle()
        {
            transform.localScale = Vector3.one;

            transform.DOPunchScale(
                punch: Vector3.one * 0.2f, // ¾ó¸¶³ª Ä¿ÁúÁö
                duration: 0.2f,            // ÀüÃ¼ ½Ã°£
                vibrato: 1,                // Èçµé¸² È½¼ö
                elasticity: 0.8f           // Æ¨±è Á¤µµ
            );

            Initialize();
        }
        protected abstract void Initialize();
        public abstract void OnPlayerReached();
        public virtual void Destroy()
        {
            transform.DOKill();
            IsDestroyed = true;
            spriteRen.enabled = false;
            deadParticle.Play();
            Destroy(gameObject, deadParticle.main.duration);
        }

        public string GetObstacleDesc()
        {
            return obstacleSO.Desc;
        }
    }
}