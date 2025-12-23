using DG.Tweening;
using UnityEngine;

namespace Hwan
{
    public class BouncePowerChangerObstacle : Obstacle
    {
        [SerializeField] private float bouncePower;

        protected override void Initialize()
        {

        }

        public override void OnPlayerReached()
        {
            Player player = GameManager.Instance.Player;
            player.MovePower *= bouncePower;
        }
    }
}