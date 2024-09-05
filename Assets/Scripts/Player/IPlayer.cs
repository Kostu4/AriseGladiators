using Scripts.EnemyScript;

namespace Scripts.PlayerScript
{
    public interface IPlayer
    {
        float PlayerDamage { get; set; }
        float PlayerHealth { get; set; }

        void AttackEnemy(IEnemy enemy);
    }
}