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
            Debug.Log("플레이어 각도 틀기");
        }
    }
}

