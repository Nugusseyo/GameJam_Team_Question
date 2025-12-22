namespace Hwan
{
    using UnityEngine;
    public class SlowEnemyObstacle : CountObstacle
    {
        [SerializeField] private float slowAmount;

        protected override void CountObsInitialize()
        {
            
        }

        protected override void OnPlayerReachedOnce()
        {
            Debug.Log("Enemy 슬로우 뭍히기");
        }
    }
}
