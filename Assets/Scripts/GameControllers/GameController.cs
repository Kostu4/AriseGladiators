using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Scripts.PlayerScript;
using Scripts.EnemyScript;

public class GameController : MonoBehaviour
{
    #region Objects
    [Header("Game objects")]
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Enemy[] enemyPrefabs;
    [SerializeField] private Transform spawnEnemyPoint;
    [SerializeField] private Transform spawnPlayerPoint;
    [SerializeField] private float spawnDelay = 2f;
    [SerializeField] private TMP_Text killCountText;
    [SerializeField] private GameObject endRoundCanvas;

    public readonly UnityEvent killLimitReached = new();
    public bool isPaused = false;
    public bool isReadyToStart = false;

    private Player player;
    private Enemy currentEnemy;
    #endregion

    private void Awake()
    {
        SpawnPlayer();
        SpawnEnemy();

        killLimitReached.AddListener(EndRound);
        endRoundCanvas.SetActive(false);
        killCountText.text = $"{LevelManager.Instance.enemiesKilled}/{LevelManager.Instance.enemiesToKill}";
    }

    #region Spawning
    private void SpawnPlayer()
    {
        if (playerPrefab != null)
        {
            player = Instantiate(playerPrefab, spawnPlayerPoint.position, Quaternion.identity);
        }
    }

    private void SpawnEnemy()
    {
        currentEnemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnEnemyPoint.position, Quaternion.identity);
        currentEnemy.OnDeath.AddListener(enemy => HandleEnemyDeath(enemy));
        currentEnemy.OnClicked.AddListener(() => AttackEnemy(currentEnemy));
    }
    #endregion
    
    public void AttackEnemy(IEnemy enemy)
    {
        if (enemy != null && !isPaused)
        {
            enemy.TakeDamage(player.PlayerDamage);
        }
    }

    #region DieEnemyInvoke
    private void HandleEnemyDeath(IEnemy enemy)
    {
        if (isPaused == false)
        {
            LevelManager.Instance.OnEnemyKilled();
            killCountText.text = $"{LevelManager.Instance.enemiesKilled}/{LevelManager.Instance.enemiesToKill}";

            StartCoroutine(RespawnEnemy(spawnDelay));
        }
    }

    private IEnumerator RespawnEnemy(float delay)
    {
        Debug.Log($"Respawning enemy...");
        yield return new WaitForSeconds(delay);

        // Проверка на окончание уровня или паузу
        if (!isPaused)
        {
            Debug.Log("Respawned enemy");
            SpawnEnemy();
        }
    }
    #endregion

    public void EndRound()
    {
        endRoundCanvas.SetActive(true);
        PauseGame();
        StartCoroutine(ChangeScene());
    }

    public void ReadyToNextLevel()
    {
        isReadyToStart = true;
    }

    private IEnumerator ChangeScene()
    {
        while (!isReadyToStart)
        {
            //Debug.LogWarning("Waiting for isReadyToStart");
            yield return null;
        }
        //Debug.LogWarning("Reload");
        LevelManager.Instance.ReloadScene();
    }

    #region Pause
    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
    }
    #endregion
}