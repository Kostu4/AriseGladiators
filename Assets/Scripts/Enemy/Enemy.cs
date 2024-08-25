using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Scripts.PlayerScript;

namespace Scripts.EnemyScript
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float maxEnemyHealth;
        private float currentEnemyHealth;
        private GameController gameController;
        public void SetGameController(GameController controller) //баян - переделать
        {
            gameController = controller;
        }

        private void Awake()
        {
            currentEnemyHealth = maxEnemyHealth;
        }

        public void TakeDamage(float damage)
        {
            currentEnemyHealth -= damage;
            if (currentEnemyHealth <= 0)
            {
                EnemyDie();
            }
        }
        public void EnemyDie()
        {
            if (gameController != null)
            {
                gameController.isEnemyDead.Invoke(this);
            }
            Destroy(gameObject);
        }
    }
}

