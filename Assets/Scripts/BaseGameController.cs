using System.Collections;
using UnityEngine;
using Scripts.PlayerScript;
using Scripts.EnemyScript;
using UnityEditor.Build.Content;

public class BaseGameController : MonoBehaviour
{
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Enemy[] enemyPrefabs;
    [SerializeField] private Transform spawnEnemyPoint;
    [SerializeField] private Transform spawnPlayerPoint;
    [SerializeField] private float spawnDelay = 2f;

    public bool isPaused = false;

    protected Player player;
    private Enemy currentEnemy;
    private EnemySpawner enemySpawner;

    protected virtual void Awake()
    {
        SpawnPlayer();
        enemySpawner = new EnemySpawner(enemyPrefabs, spawnEnemyPoint, HandleEnemyDeath);
        SpawnEnemy();
    }

    protected void SpawnPlayer()
    {
        if (playerPrefab != null)
        {
            player = Instantiate(playerPrefab, spawnPlayerPoint.position, Quaternion.identity);
        }
    }

    protected void SpawnEnemy()
    {
        currentEnemy = enemySpawner.SpawnEnemy();
        if (currentEnemy != null)
        {
            currentEnemy.OnClicked.AddListener(() => AttackEnemy(currentEnemy));
        }
    }
    public void AttackEnemy(IEnemy enemy)
    {
        if (enemy != null)
        {
            enemy.TakeDamage(player.PlayerDamage);
        }
    }

    protected virtual void HandleEnemyDeath(IEnemy enemy)
    {
        StartCoroutine(RespawnEnemy(spawnDelay));
    }

    protected IEnumerator RespawnEnemy(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnEnemy();
    }

    public void PauseGame()
    {
        isPaused = true; Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        isPaused = false; Time.timeScale = 1;
    }
}