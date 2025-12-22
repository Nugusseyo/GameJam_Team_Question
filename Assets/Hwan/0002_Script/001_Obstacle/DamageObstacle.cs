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
                SpawnObstacle();
            }
        }

        protected override void OnPlayerReachedOnce()
        {
            switch (damagedType)
            {
                case ObstacleDamagedType.None:
                    Debug.Log("None 이잔아ㅏ");
                    break;
                case ObstacleDamagedType.Player:
                    //Player에게 데미지를 입히자
                    break;
                case ObstacleDamagedType.Enemy:
                    //Enemy에게 데미지를 입히자
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
