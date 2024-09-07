using UnityEngine.Events;

namespace Scripts.EnemyScript
{
    public interface IEnemy
    {
        float MaxEnemyHealth { get; }
        float CurrentEnemyHealth { get; }
        int EnemyCoins { get; }

        //int ExpAmount { get; }

        UnityEvent<IEnemy> OnDeath { get; } // Событие смерти
        UnityEvent OnClicked { get; } // Событие клика
        void TakeDamage(float damage);
    }
}