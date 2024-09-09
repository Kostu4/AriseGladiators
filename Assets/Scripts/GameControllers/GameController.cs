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
    //[SerializeField] private Transform container;
    //[SerializeField] private GameObject[] buffCardsPrefabs;

    public readonly UnityEvent killLimitReached = new();
    public bool isPaused = false;
    public bool isReadyToStart = false;

    private Player player;
    private Enemy currentEnemy;
    private Coroutine respawnCoroutine;
    #endregion

    private void Awake()
    {
        SpawnPlayer();
        SpawnEnemy();

        killLimitReached.AddListener(EndRound);
        endRoundCanvas.SetActive(false);
        UpdateKillCountText();
    }

    #region Spawning
    private void SpawnPlayer()
    {
        if (playerPrefab != null)
        {
            player = Instantiate(playerPrefab, spawnPlayerPoint.position, Quaternion.identity);
        }
    }

    public void SpawnEnemy()
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
        if (!isPaused)
        {
            LevelManager.Instance.OnEnemyKilled();
            UpdateKillCountText();

            StartCoroutine(RespawnEnemy(spawnDelay));
        }
    }

    private IEnumerator RespawnEnemy(float delay)
    {
        Debug.Log("Respawn");
        yield return new WaitForSeconds(delay);
        if (!isPaused)
        {
            SpawnEnemy();
            Debug.Log("Spawn");
        }
    }
    #endregion

    private void UpdateKillCountText()
    {
        killCountText.text = $"{LevelManager.Instance.enemiesKilled}/{LevelManager.Instance.enemiesToKill}";
    }

    public void EndRound()
    {
        if (respawnCoroutine != null)
        {
            StopCoroutine(RespawnEnemy(spawnDelay));
        }

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
            yield return null;
        }
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