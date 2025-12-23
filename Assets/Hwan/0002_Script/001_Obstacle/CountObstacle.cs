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
            base.OnPlayerReached();
            OnPlayerReachedOnce();
            currentCount++;
            textMeshPro.text = (count - currentCount).ToString();
            if (currentCount >= count)
            {
                Destroy();
            }
        }

        public override void SpawnObstacle(Vector2 normalVector)
        {
            base.SpawnObstacle(normalVector);
            textMeshPro.transform.position += (Vector3)normalVector/2;
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
            desc = desc.Replace("{c}", (count - currentCount).ToString());
            return desc;
        }

        public override void Destroy()
        {
            if (IsDestroyed) return;
            base.Destroy();
            textMeshPro.enabled  = false;
        }
    }
}

