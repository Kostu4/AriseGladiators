using Scripts.EnemyScript;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int currentLevel = 1;
    [SerializeField] public int enemiesToKill = 5;
    [SerializeField] public int enemiesKilled = 0;

    private GameController gameController;
    private Enemy enemy;

    public int enemyHealthIncrease = 10;

    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void OnEnemyKilled()
    {
        enemiesKilled++;
        if (enemiesKilled >= enemiesToKill)
        {
            gameController = FindObjectOfType<GameController>();
            gameController.killLimitReached.Invoke();
        }
    }

    private void AdvanceToNextLevel()
    {
        currentLevel++;
        Debug.Log($"Level: 1-{currentLevel}");
        enemiesKilled = 0;

        if (currentLevel % 5 == 0)
        {
            enemiesToKill++;
        }
        IncreaseEnemyStats();
    }

    private void IncreaseEnemyStats()
    {
        if (enemy != null)
        {
            enemy.IncreaseHealth(enemyHealthIncrease);
            //enemy.IncreaseDamage(enemyDamageIncrease);
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "InstanceScene")
        {
            gameController = FindObjectOfType<GameController>();
            enemy = FindObjectOfType<Enemy>();
            AdvanceToNextLevel();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}