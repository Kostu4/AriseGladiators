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


        private GameController gameController;
        public DamageTextScript damageText;
        public void SetGameController(GameController controller) //баян - переделать
        {
            gameController = controller;
        }

        private void Awake()
        {
            currentEnemyHealth = maxEnemyHealth;
            enemyHealthSlider.maxValue = maxEnemyHealth;
            enemyHealthSlider.value = currentEnemyHealth;
        }

        public void TakeDamage(float damage)
        {
            currentEnemyHealth -= damage;
            enemyHealthSlider.value = currentEnemyHealth;

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

