using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Collections;

namespace Scripts.EnemyScript
{
    public class Enemy : MonoBehaviour
    {
        [Header("Enemy Settings")]
        [SerializeField] private float maxEnemyHealth;
        [SerializeField] public Slider enemyHealthSlider;
        

        public UnityEvent OnEmenyClick = new();
        public float MaxEnemyHealth => maxEnemyHealth;
        public float CurrentEnemyHealth { get; set; }
        //public int EnemyCoins => enemyCoins;


        private void Awake()
        {
            InitializeHealth();
        }

        private void InitializeHealth()
        {
            CurrentEnemyHealth = maxEnemyHealth;
            enemyHealthSlider.maxValue = maxEnemyHealth;
            enemyHealthSlider.value = CurrentEnemyHealth;
        }

        public void OnMouseDown()
        {
            if (CompareTag("Enemy"))
            { 
                OnEmenyClick.Invoke();
            }
        }

        public void IncreaseHealth(int value)
        { 
            maxEnemyHealth += value;
            CurrentEnemyHealth = maxEnemyHealth;
            enemyHealthSlider.maxValue = maxEnemyHealth;
            enemyHealthSlider.value = CurrentEnemyHealth;
        }
    }
}