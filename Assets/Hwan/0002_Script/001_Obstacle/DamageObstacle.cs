using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hwan
{
    public class DamageObstacle : CountObstacle
    {
        [SerializeField] private int damage;
        [SerializeField] private ObstacleDamagedType damagedType;

        private void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                OnPlayerReached();
            }
            else if (Keyboard.current.aKey.wasPressedThisFrame) 
            {
                SpawnObstacle(Vector2.up);
            }
        }

        protected override void OnPlayerReachedOnce()
        {
            switch (damagedType)
            {
                case ObstacleDamagedType.None:
                    Debug.Log("None ¿Ã¿‹æ∆§ø");
                    break;
                case ObstacleDamagedType.Player:
                    GameManager.Instance.Player.HealthSystem.GetDamage(damage);
                    break;
                case ObstacleDamagedType.Enemy:
                    //EnemyManager.Instance.MinusEnemyHealth();
                    //Enemy
                    break;
            }
        }

        protected override void CountObsInitialize()
        {
        }

        public override string GetObstacleDesc()
        {
            string desc = base.GetObstacleDesc();
            desc = desc.Replace("{t}", damagedType.ToString());
            desc = desc.Replace("{n}", damage.ToString());
            desc = desc.Replace("{c}", currentCount.ToString());
            return desc;
        }
    }
}
