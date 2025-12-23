using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.Events;
using System.Collections;

namespace Assets.JYG._Script
{
    public abstract class Enemy : MonoBehaviour
    {
        public EnemyHealthShowcase healthShowcase;
        protected Transform _target;
        protected Sequence _moveTween;
        private Collider2D _collider;
        bool _isDead = false;

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
                if(_isDead) return;
                if (value <= 0)
                {
                    if(_currentHealth != 0)
                    {
                        EnemyStun?.Invoke();
                    }
                    _currentHealth = 0;
                    OnHealthChange?.Invoke();
                    return;
                }
                else if(value > MaxHealth)
                {
                    _currentHealth = MaxHealth;
                    OnHealthChange?.Invoke();
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
                OnHealthChange?.Invoke();
            }
        }

        public Action OnHealthChange;
        public UnityEvent EnemyStun;
        public UnityEvent EnemyDead;
        public UnityEvent Enemy2Exp;
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
            if(healthShowcase != null)
            {
                Destroy(healthShowcase.gameObject);
            }
            if (CurrentHealth > 0)
            {
                GameManager.Instance.Player.HealthSystem.GetDamage(1);
            }
            _isDead = true;
            _collider.enabled = false;

            StopEnemy();
            transform.DOPause();
            transform.DOKill();

            if(CurrentHealth <= 0)
                Enemy2Exp?.Invoke();
            else
                EnemyDead?.Invoke();

            Destroy(gameObject, 2f);
            GameManager.Instance.Player.Level.GetExp(1);
        }
        private void OnDestroy()
        {
            transform.DOKill();
        }
    }
}