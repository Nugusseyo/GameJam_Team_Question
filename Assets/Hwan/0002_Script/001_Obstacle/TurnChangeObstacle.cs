namespace Hwan
{
    using UnityEngine;

    public class TurnChangeObstacle : CountObstacle
    {
        [SerializeField] private bool isPass;

        protected override void CountObsInitialize()
        {
            
        }

        protected override void OnPlayerReachedOnce()
        {
            if (isPass == true)
            {
                GameManager.Instance.TurnManager.PlusTurn(1);
            }
            else
            {
                GameManager.Instance.TurnManager.MinusTurn(1);
            }
        }
    }
}
