namespace Hwan
{
    using UnityEngine;

    public abstract class CountObstacle : Obstacle
    {
        [SerializeField] private int count;
        [SerializeField] private int currentCount;
        public sealed override void OnPlayerReached()
        {
            OnPlayerReachedOnce();
            currentCount++;
            if (currentCount >= count)
            {
                Destroy();
            }
        }

        protected abstract void OnPlayerReachedOnce();

        protected sealed override void Initialize()
        {
            currentCount = 0;
        }

        protected abstract void CountObsInitialize();
    }
}

