using System.Collections;
using UnityEngine;
using Scripts.PlayerScript;
using Scripts.EnemyScript;

public class GameController : MonoBehaviour
{
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Enemy[] enemyPrefabs;
    [SerializeField] private Transform spawnEnemyPoint;
    [SerializeField] private Transform spawnPlayerPoint;
    [SerializeField] private float spawnDelay = 2f;

    private Player player;
    private Enemy currentEnemy;
    private EnemySpawner enemySpawner;

    private void Awake()
    {
        SpawnPlayer();
        enemySpawner = new EnemySpawner(enemyPrefabs, spawnEnemyPoint, HandleEnemyDeath);
        SpawnEnemy();
    }

    private void SpawnPlayer()
    {
        if (playerPrefab != null)
        {
            player = Instantiate(playerPrefab, spawnPlayerPoint.position, Quaternion.identity);
        }
    }

    private void SpawnEnemy()
    {
        currentEnemy = enemySpawner.SpawnEnemy();
        if (currentEnemy != null)
        {
            currentEnemy.OnCoinsGained += player.AddCoins; // Подписка на получение монет
            currentEnemy.OnClicked += () => player.AttackEnemy(currentEnemy); // Подписка на клик
        }
    }

    private void HandleEnemyDeath(IEnemy enemy)
    {
        StartCoroutine(RespawnEnemy(spawnDelay));
    }

    private IEnumerator RespawnEnemy(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnEnemy();
    }
}