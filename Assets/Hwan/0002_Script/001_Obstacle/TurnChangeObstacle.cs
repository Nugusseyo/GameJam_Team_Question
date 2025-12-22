namespace Hwan
{
    using UnityEngine;

    public class TurnChangeObstacle : CountObstacle
    {
        [SerializeField] private int changeTurnAmount;

        protected override void CountObsInitialize()
        {
            
        }

        protected override void OnPlayerReachedOnce()
        {
            Debug.Log("턴 바꾸기");
        }
    }
}
