using UnityEngine;
using Scripts.EnemyScript;

namespace Scripts.PlayerScript
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float currentPlayerDamage;

        public void AttackEnemy(Enemy enemy)
        { 
            if(enemy != null)
            {
                enemy.TakeDamage(currentPlayerDamage);
            }
        }
    }
}

