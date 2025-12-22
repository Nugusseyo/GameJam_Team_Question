using DG.Tweening;
using UnityEngine;

namespace Hwan
{
    public class RandomBounceObstacle : Obstacle
    {
        [SerializeField] private float bouncePower;

        protected override void Initialize()
        {

        }

        public override void OnPlayerReached()
        {
            Debug.Log("플레이어 바운스 파워 바꾸기");
        }
    }

}
