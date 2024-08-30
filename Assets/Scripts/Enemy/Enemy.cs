using Scripts.PlayerScript;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.EnemyScript
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        [Header("Enemy Settings")]
        [SerializeField] private float maxEnemyHealth;
        [SerializeField] private Slider enemyHealthSlider;
        [SerializeField] private int enemyCoins;

        public float MaxEnemyHealth => maxEnemyHealth;
        public float CurrentEnemyHealth { get; private set; }
        public int EnemyCoins => enemyCoins;

        public event Action<IEnemy> OnDeath;
        public event Action<int> OnCoinsGained;
        public event Action OnClicked;

        private void Awake()
        {
            InitializeHealth();
        }

        private void InitializeHealth()
        {
            CurrentEnemyHealth = maxEnemyHealth;
            enemyHealthSlider.maxValue = maxEnemyHealth;
            enemyHealthSlider.value = CurrentEnemyHealth;
        }

        private void OnMouseDown()
        {
            OnClicked?.Invoke(); // Сообщение о клике
        }

        public void TakeDamage(float damage)
        {
            CurrentEnemyHealth -= damage;
            enemyHealthSlider.value = CurrentEnemyHealth;

            if (CurrentEnemyHealth <= 0)
            {
                OnCoinsGained?.Invoke(enemyCoins); // Уведомление о передаче монет
                Die();
            }
        }

        private void Die()
        {
            OnDeath?.Invoke(this); // Уведомление о смерти врага
            Destroy(gameObject);
        }
    }
}