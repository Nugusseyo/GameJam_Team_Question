namespace Hwan
{
    using TMPro;
    using UnityEngine;

    public abstract class CountObstacle : Obstacle
    {
        [SerializeField] private int count;
        private TextMeshPro textMeshPro;
        protected int currentCount;

        public sealed override void OnPlayerReached()
        {
            OnPlayerReachedOnce();
            currentCount++;
            textMeshPro.text = (count - currentCount).ToString();
            if (currentCount >= count)
            {
                Destroy();
            }
        }

        protected abstract void OnPlayerReachedOnce();

        protected sealed override void Initialize()
        {
            currentCount = 0;
            textMeshPro = transform.GetChild(1).GetComponent<TextMeshPro>();    
            textMeshPro.text = (count - currentCount).ToString();
            CountObsInitialize();
        }

        protected abstract void CountObsInitialize();

        public override string GetObstacleDesc()
        {
            string desc = base.GetObstacleDesc();
            desc = desc.Replace("{c}", currentCount.ToString());
            return desc;
        }
    }
}

