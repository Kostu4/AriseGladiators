using UnityEngine;
using Scripts.EnemyScript;
using System.Collections;
using System.Collections.Generic;

namespace Scripts.PlayerScript
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float playerDamage;
        [SerializeField] private float playerHealth;
        private bool isLife => playerHealth > 0;
        public int playerCoins;

        public void AttackEnemy(Enemy enemy)
        {
            if (enemy != null)
            {
                enemy.TakeDamage(playerDamage);
            }
        }
    }
}
