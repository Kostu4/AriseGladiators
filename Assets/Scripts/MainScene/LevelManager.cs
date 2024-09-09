using Scripts.EnemyScript;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int currentLevel = 1;
    [SerializeField] public int enemiesToKill = 5;
    [SerializeField] public int enemiesKilled = 0;

    private GameController gameController;

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

    public void SetupLevel(int LevelIndex)
    { 
        currentLevel = LevelIndex;
        AdvanceToNextLevel();
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
        enemiesKilled = 0;

        if (currentLevel % 5 == 0)
        {
            enemiesToKill++;
        }
        
        IncreaseEnemyStats();
    }

    private void IncreaseEnemyStats()
    {
        var enemies = FindObjectsOfType<Enemy>();
        foreach (var enemy in enemies)
        {
            enemy.IncreaseHealth(currentLevel * enemyHealthIncrease);
            //enemy.IncreaseDamage(enemyDamageIncrease);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainScene");
    }
}