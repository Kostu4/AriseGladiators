using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Scripts.PlayerScript;
using Scripts.EnemyScript;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    #region Objects
    [Header("Game objects")]
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Enemy[] enemyPrefabs;
    [SerializeField] private Transform spawnEnemyPoint;
    [SerializeField] private Transform destinationEnemyPoint;
    [SerializeField] private float moveSpeedEnemy;
    [SerializeField] private Transform spawnPlayerPoint;
    //[SerializeField] private float spawnDelay = 0.5f;
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
        Debug.Log("Spawn");
        int randomIndex = Random.Range(0, 101);
        if (randomIndex <= 80)
        {
            currentEnemy = Instantiate(enemyPrefabs[0], spawnEnemyPoint.position, Quaternion.identity);
            currentEnemy.OnEmenyClick.AddListener(() => AttackEnemy(currentEnemy));
            StartCoroutine(MoveEnemyToDestination(currentEnemy));
        }
        else
        {
            currentEnemy = Instantiate(enemyPrefabs[1], spawnEnemyPoint.position, Quaternion.identity);
            currentEnemy.OnEmenyClick.AddListener(() => AttackEnemy(currentEnemy));
            StartCoroutine(MoveEnemyToDestination(currentEnemy));
        }

        Debug.Log("Spawned");
    }

    IEnumerator MoveEnemyToDestination(Enemy enemy)
    {
        while (enemy != null && !isPaused)
        { 
            if (enemy.transform.position != destinationEnemyPoint.position)
            {
                Debug.Log("Move");
                enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, destinationEnemyPoint.position, moveSpeedEnemy * Time.deltaTime);
            }
            yield return null;
            Debug.Log("Reached");
        }
    }
    #endregion

    public void AttackEnemy(Enemy enemy)
    {
        if (enemy != null && !isPaused)
        {
            TakeDamage(player.PlayerDamage);
        }
    }

    public void TakeDamage(int damage)
    { 
        currentEnemy.CurrentEnemyHealth -= damage;
        currentEnemy.enemyHealthSlider.value = currentEnemy.CurrentEnemyHealth;

        if (currentEnemy.CurrentEnemyHealth <= 0)
        {
            EnemyDie();
        }
    }
    public void EnemyDie()
    {
        Destroy(currentEnemy.gameObject);
        currentEnemy.OnEmenyClick.RemoveListener(() => AttackEnemy(currentEnemy));
        LevelManager.Instance.OnEnemyKilled();
        UpdateKillCountText();
        if (!isPaused)
        {
            SpawnEnemy();
        }
    }

    private void UpdateKillCountText()
    {
        killCountText.text = $"{LevelManager.Instance.enemiesKilled}/{LevelManager.Instance.enemiesToKill}";
    }

    public void EndRound()
    {
        endRoundCanvas.SetActive(true);
        PauseGame();
        StartCoroutine(ChangeScene());
    }

    public void ReadyToNextLevel()
    {
        isReadyToStart = true;
        Debug.Log("ReadyToNextLevel");
    }

    private IEnumerator ChangeScene()
    {
        while (!isReadyToStart)
        {
            Debug.Log("Waiting");
            yield return null;
        }
        LevelManager.Instance.LoadMainMenu();
        Debug.Log("ChangeScene");
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