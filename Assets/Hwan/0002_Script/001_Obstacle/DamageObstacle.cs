using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hwan
{
    public class DamageObstacle : CountObstacle
    {
        [SerializeField] private int maxDamage = 2;
        private int damage;
        [SerializeField] private ObstacleDamagedType damagedType;

        protected override void OnPlayerReachedOnce()
        {
            switch (damagedType)
            {
                case ObstacleDamagedType.None:
                    Debug.Log("None 이잔아ㅏ");
                    break;
                case ObstacleDamagedType.Player:
                    if (damage > 0)
                    {
                        GameManager.Instance.Player.HealthSystem.GetDamage(damage);
                    }
                    else
                    {
                        GameManager.Instance.Player.HealthSystem.GetHeal(damage);
                    }
                    break;
                case ObstacleDamagedType.Enemy:
                    EnemyManager.Instance.PlusEnemyHealth(damage);
                    break;
            }
        }

        protected override void CountObsInitialize()
        {
            damage = Random.Range(1, maxDamage + 1);
        }

        public override string GetObstacleDesc()
        {
            string desc = base.GetObstacleDesc();
            desc = desc.Replace("{d}", damage.ToString());
            return desc;
        }
    }
}
