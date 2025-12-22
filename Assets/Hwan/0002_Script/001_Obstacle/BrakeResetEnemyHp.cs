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

        public override string GetObstacleDesc()
        {
            string desc = base.GetObstacleDesc();
            desc = desc.Replace("{t}", 1.ToString());
            return desc;
        }
    }
}
