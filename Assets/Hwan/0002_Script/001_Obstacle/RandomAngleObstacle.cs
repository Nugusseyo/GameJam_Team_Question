using UnityEngine;

namespace Hwan
{
    public class RandomAngleObstacle : Obstacle
    {
        [SerializeField] private float maxAngle;
        private float angle;

        protected override void Initialize()
        {
        }

        public override void OnPlayerReached()
        {
            float randomAngle = Random.Range(-maxAngle, maxAngle);
            float baseRad = Mathf.Atan2(normalVector.y, normalVector.x);
            float rad = baseRad + randomAngle * Mathf.Deg2Rad;

            Vector2 bounceDir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
            GameManager.Instance.Player.RandomBounce = bounceDir;

        }
    }
}

