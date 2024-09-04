using System;

namespace Scripts.EnemyScript
{
    public interface IEnemy
    {
        float MaxEnemyHealth { get; }
        float CurrentEnemyHealth { get; }
        int EnemyCoins { get; }

        event Action<IEnemy> OnDeath; //Событие смерти
        event Action<int> OnCoinsGained; //получения монет
        event Action OnClicked; // Делегат для обработки клика

        void TakeDamage(float damage);
    }
}