using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.Events;
using System.Collections;

namespace Assets.JYG._Script
{
    public abstract class Enemy : MonoBehaviour
    {
        protected Transform _target;
        protected Sequence _moveTween;
        private Collider2D _collider;

        [field: SerializeField] public float Duration { get; protected set; } = 3f;
        [field: SerializeField] public float Distance { get; protected set; } = 2f;
        [field: SerializeField] public float MoveDelay { get; protected set; } = 1f;
        [field: SerializeField] public int MaxHealth { get; protected set; } = 1;
        protected int _currentHealth = 1;
        public int CurrentHealth
        {
            get => _currentHealth;

            set
            {
                if (value <= 0)
                {
                    if(_currentHealth != 0)
                    {
                        EnemyStun?.Invoke();
                    }
                    _currentHealth = 0;
                    return;
                }
                else if(value > MaxHealth)
                {
                    _currentHealth = MaxHealth;
                    return;
                }
                if(_currentHealth == 0 && value > 0)
                {
                    EnemyLive?.Invoke();
                }
                else if(value > _currentHealth)
                {
                    EnemyHeal?.Invoke();
                }
                _currentHealth = value;
            }
        }

        public Action EnemyStun;
        public UnityEvent EnemyDead;
        public UnityEvent EnemyHeal;
        public UnityEvent EnemyLive;

        [field: SerializeField] public Ease Ease { get; set; } = Ease.Linear;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        private void Start()
        {
            EnemyInitialize();
            _target = GameObject.Find("Player").transform;
            EnemyMove();

            EnemyStun += () => Debug.Log("Enemy Stun");
        }
        public void EnemyInitialize()
        {
            Debug.Log("Enemy Spanwned");
            EnemyManager.Instance.AddEnemy(this);
            CurrentHealth = MaxHealth;
        }
        protected abstract void EnemyMove();
        public void StopEnemy()
        {
            _moveTween.Pause();
            _moveTween.Kill();
        }
        public void StartMoveEnemy()
        {
            EnemyMove();
        }
        private void OnDisable()
        {
            EnemyManager.Instance.RemoveEnemy(this);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            DeadEnemy();
            Debug.Log("플레이어 감지됨");
        }

        private void DeadEnemy()
        {
            _collider.enabled = false;

            StopEnemy();
            transform.DOPause();
            transform.DOKill();

            EnemyDead?.Invoke();

            Destroy(gameObject, 2f);
        }
    }
}