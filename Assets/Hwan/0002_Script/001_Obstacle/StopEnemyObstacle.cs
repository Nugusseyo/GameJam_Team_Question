namespace Hwan
{
    using UnityEngine;
    public class StopEnemyObstacle : CountObstacle
    {
        [SerializeField] private int stopTurn = 2;

        protected override void CountObsInitialize()
        {
            
        }

        protected override void OnPlayerReachedOnce()
        {
            EnemyManager.Instance.MoveSkipTurn = stopTurn;
        }
    }
}
