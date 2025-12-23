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
            base.OnPlayerReached();
            float randomAngle = Random.Range(-maxAngle, maxAngle);
            float baseAngle = Mathf.Atan2(-normalVector.y, -normalVector.x) * Mathf.Rad2Deg;
            float rad = (randomAngle + baseAngle) * Mathf.Deg2Rad;

            Vector2 bounceDir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
            GameManager.Instance.Player.MoveDir = bounceDir;

        }
    }
}

