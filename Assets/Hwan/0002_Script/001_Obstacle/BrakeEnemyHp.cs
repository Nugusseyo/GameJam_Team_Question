using UnityEngine;

namespace Hwan
{
    public class BrakeEnemyHp : CountObstacle
    {
        protected override void CountObsInitialize()
        {
            
        }

        protected override void OnPlayerReachedOnce()
        {
            Debug.Log("에너미 리셋 끄기");
        }
    }
}
