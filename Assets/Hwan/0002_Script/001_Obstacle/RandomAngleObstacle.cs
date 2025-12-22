using UnityEngine;

namespace Hwan
{
    public class RandomAngleObstacle : Obstacle
    {
        [SerializeField] private float maxAngle;
        [SerializeField] private float angle;

        protected override void Initialize()
        {
            angle = Random.Range(-maxAngle, maxAngle);
        }

        public override void OnPlayerReached()
        {
            Vector2 playerDir = GameManager.Instance.Player.GetDir();
            float rad = Mathf.Deg2Rad * (angle + Mathf.Atan2(normalVector.y, normalVector.x));
            GameManager.Instance.Player.SetMove(new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)), playerDir.magnitude);
        }
    }
}

