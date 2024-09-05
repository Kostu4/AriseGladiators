using UnityEngine;
using Scripts.EnemyScript;

namespace Scripts.PlayerScript
{
    public class Player : MonoBehaviour, IPlayer
    {
        [SerializeField] private float playerDamage;
        [SerializeField] private float playerHealth;
        public float PlayerDamage { get => playerDamage; set => playerDamage = value; }
        public float PlayerHealth { get => playerHealth; set => playerHealth = value; }

        //public void AttackEnemy(IEnemy enemy)
        //{
        //    if (enemy != null)
        //    {
        //        enemy.TakeDamage(PlayerDamage);
        //    }
        //}
    }
}
