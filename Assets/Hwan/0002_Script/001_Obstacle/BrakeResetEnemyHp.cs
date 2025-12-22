using UnityEngine;

namespace Hwan
{
    public class BrakeResetEnemyHp : CountObstacle
    {
        protected override void CountObsInitialize() { }

        protected override void OnPlayerReachedOnce()
        {
            EnemyManager.Instance.ResetSkipTurn = 1;
        }
    }
}
