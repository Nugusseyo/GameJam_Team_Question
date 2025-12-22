using UnityEngine;

namespace Hwan
{
    public class RandomAngleObstacle : Obstacle
    {
        [SerializeField] private float maxAngle;
        private float angle;

        protected override void Initialize()
        {
            angle = Random.Range(-maxAngle, maxAngle);
        }

        public override void OnPlayerReached()
        {
            float rad = Mathf.Deg2Rad * (angle + Mathf.Atan2(normalVector.y, normalVector.x));
            Vector2 vector = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
            GameManager.Instance.Player.RandomBounce = vector;
        }
    }
}

