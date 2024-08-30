using UnityEngine;
using Scripts.EnemyScript;

namespace Scripts.PlayerScript
{
    public class Player : MonoBehaviour, IPlayer
    {
        [SerializeField] private float playerDamage;
        [SerializeField] private float playerHealth;
        [SerializeField] private int playerCoins;
        public float PlayerDamage { get => playerDamage; set => playerDamage = value; }
        public float PlayerHealth { get => playerHealth; set => playerHealth = value; }

        public int PlayerCoins { get => playerCoins; set => playerCoins = value; } // Свойство для монет

        public void AttackEnemy(IEnemy enemy)
        {
            if (enemy != null)
            {
                enemy.TakeDamage(PlayerDamage);
            }
        }

        public void AddCoins(int amount)
        {
            PlayerCoins += amount;
            // Здесь можно добавить логику сохранения монет или обновления UI
        }
    }
}
