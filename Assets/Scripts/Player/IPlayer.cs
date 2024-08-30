using Scripts.EnemyScript;

namespace Scripts.PlayerScript
{
    public interface IPlayer
    {
        float PlayerDamage { get; set; }
        float PlayerHealth { get; set; }
        int PlayerCoins { get; set; }

        void AttackEnemy(IEnemy enemy);
        void AddCoins(int amount);
    }
}