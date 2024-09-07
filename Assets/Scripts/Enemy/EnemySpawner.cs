//using System;
//using UnityEngine;

//namespace Scripts.EnemyScript
//{
//    public class EnemySpawner
//    {
//        private readonly Enemy[] enemyPrefabs;
//        private readonly Transform spawnPoint;
//        private readonly Action<IEnemy> onEnemyDeath;

//        public EnemySpawner(Enemy[] enemyPrefabs, Transform spawnPoint, Action<IEnemy> onEnemyDeath)
//        {
//            this.enemyPrefabs = enemyPrefabs;
//            this.spawnPoint = spawnPoint;
//            this.onEnemyDeath = onEnemyDeath;
//        }

//        public Enemy SpawnEnemy()
//        {
//            if (enemyPrefabs == null || enemyPrefabs.Length == 0)
//                return null;

//            int randomIndex = UnityEngine.Random.Range(0, enemyPrefabs.Length);
//            Enemy enemy = UnityEngine.Object.Instantiate(enemyPrefabs[randomIndex], spawnPoint.position, Quaternion.identity);

//            // Подписываемся на событие смерти врага
//            enemy.OnDeath.AddListener(enemy => onEnemyDeath.Invoke(enemy));

//            return enemy;
//        }

//    }
//}