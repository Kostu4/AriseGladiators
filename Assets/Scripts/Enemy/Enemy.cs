using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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

        public UnityEvent<IEnemy> OnDeath { get; } = new UnityEvent<IEnemy>(); // Событие смерти
        public UnityEvent OnClicked { get; } = new UnityEvent(); // Событие клика

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
            OnClicked.Invoke(); // Сообщение о клике
        }

        public void TakeDamage(float damage)
        {
            CurrentEnemyHealth -= damage;
            enemyHealthSlider.value = CurrentEnemyHealth;

            if (CurrentEnemyHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            OnDeath.Invoke(this); // Уведомление о смерти врага
            Destroy(gameObject);
        }
    }
}