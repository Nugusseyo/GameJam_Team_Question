using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hwan
{
    public class DamageObstacle : CountObstacle
    {
        private int damage;
        [SerializeField] private ObstacleDamagedType damagedType;
        [SerializeField] private int maxDamage; //- : -maxDamage ~ 0, + : 0 ~ maxDamage

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
                    Debug.Log("None ÀÌÀÜ¾Æ¤¿");
                    break;
                case ObstacleDamagedType.Player:
                    GameManager.Instance.Player.HealthSystem.GetDamage(damage);
                    break;
                case ObstacleDamagedType.Enemy:
                    //Enemy
                    break;
            }
        }

        protected override void CountObsInitialize()
        {
            int tempDmg = Random.Range(0, maxDamage);
            damage = maxDamage > 0 ? tempDmg : -tempDmg;
        }
    }
}
