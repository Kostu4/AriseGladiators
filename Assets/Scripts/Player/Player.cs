using UnityEngine;
using Scripts.EnemyScript;
using System.Collections;

namespace Scripts.PlayerScript
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float playerDamage;
        [SerializeField] private float playerHealth;
        private bool isLife => playerHealth > 0;

        private void OnMouseDown()
        {
            if (gameObject.CompareTag("Enemy"))
            { 
                AttackEnemy(gameObject.GetComponent<Enemy>());
            }
        }
        public void AttackEnemy(Enemy enemy)
        { 
            if(enemy != null)
            {
                enemy.TakeDamage(playerDamage);
            }
        }
    }
}

