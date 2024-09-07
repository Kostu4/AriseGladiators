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

    private GameController GameController;
    private Enemy enemy;

    public int enemyHealthIncrease = 10;
    //public int enemyDamageIncrease = 2;

    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OnEnemyKilled()
    { 
        enemiesKilled++;
        if (enemiesKilled >= enemiesToKill)
        {
            GameController = FindObjectOfType<GameController>();
            GameController.killLimitReached.Invoke();
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
        //IncreaseEnemyStats();
    }

    private void IncreaseEnemyStats()
    {
        //enemy.IncreaseDamage(enemyDamageIncrease);
        enemy.IncreaseHealth(enemyHealthIncrease);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AdvanceToNextLevel();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "InstanceScene")
        {
            enemy = FindObjectOfType<Enemy>();
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
