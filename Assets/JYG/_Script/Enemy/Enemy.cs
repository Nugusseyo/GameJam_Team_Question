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
        protected int _currentHealth = 0;
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
                if(_currentHealth > value)
                {
                    EnemyDamaged?.Invoke();
                }
                else if(_currentHealth < value)
                {
                    EnemyHeal?.Invoke();
                }
                _currentHealth = value;
            }
        }

        public UnityEvent EnemyStun;
        public UnityEvent EnemyDead;
        public UnityEvent EnemyHeal;
        public UnityEvent EnemyLive;
        public UnityEvent EnemyDamaged;

        [field: SerializeField] public Ease Ease { get; set; } = Ease.Linear;

        protected virtual void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        protected virtual void Start()
        {
            EnemyInitialize();
            _target = GameManager.Instance.Player.transform;
            EnemyMove();
        }
        public void EnemyInitialize()
        {
            EnemyManager.Instance.AddEnemy(this);
            CurrentHealth = MaxHealth + 1;
        }
        protected abstract void EnemyMove();
        public virtual void StopEnemy()
        {
            _moveTween.Pause();
        }
        public virtual void StartMoveEnemy()
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
        private void OnDestroy()
        {
            transform.DOKill();
        }
    }
}