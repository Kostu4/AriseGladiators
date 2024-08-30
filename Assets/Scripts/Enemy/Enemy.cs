using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Scripts.PlayerScript;
using TMPro;
using System;

namespace Scripts.EnemyScript
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float maxEnemyHealth;
        private float currentEnemyHealth;
        [SerializeField] private Slider enemyHealthSlider;
        [SerializeField] private int enemyCoins;


        private GameController gameController;
        public Player player;
        public void SetGameController(GameController controller) //баян - переделать
        {
            gameController = controller;
        }

        private void Awake()
        {
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
            currentEnemyHealth = maxEnemyHealth;
            enemyHealthSlider.maxValue = maxEnemyHealth;
            enemyHealthSlider.value = currentEnemyHealth;
        }

        private void OnMouseDown()
        {
            if (gameObject.CompareTag("Enemy"))
            {;
                player.AttackEnemy(gameObject.GetComponent<Enemy>());
            }
        }

        public void TakeDamage(float damage)
        {
            float currentdamage= UnityEngine.Random.Range(damage*0.8f, damage*1.2f);
            int currentDamage = (int)currentdamage;
            currentEnemyHealth -= currentDamage;
            enemyHealthSlider.value = currentEnemyHealth;
            if (currentEnemyHealth <= 0)
            {
                GainCoin(enemyCoins);
                EnemyDie();
            }
        }

        private void GainCoin(int enemyCoins)
        {
            player.playerCoins += enemyCoins;
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

