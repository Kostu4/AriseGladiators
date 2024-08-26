using Scripts;
using Scripts.EnemyScript;
using Scripts.PlayerScript;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class GameController : MonoBehaviour
{
    [SerializeField] Player playerPrefab;
    [SerializeField] Enemy[] enemyPrefabs;
    private Enemy currentEnemy;
    [SerializeField] Transform spawnPoint;
    [SerializeField] private float spawnDelay;
    public UnityEvent<Enemy> isEnemyDead = new();

    private void Awake()
    {
        if (enemyPrefabs != null)
        {
            SpawnEnemy();
        }
        isEnemyDead.AddListener(EnemyDeath);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerPrefab.AttackEnemy(currentEnemy);
        }
    }
    private void SpawnEnemy()
    {
        Debug.LogWarning("Spawning...");
        int enemyChance = UnityEngine.Random.Range(0, 101);
        Debug.Log("enemyChance: " + enemyChance);
        if (enemyChance <= 20)
        {
            currentEnemy = Instantiate(enemyPrefabs[1], spawnPoint.position, Quaternion.identity);
        }
        else
        {
            currentEnemy = Instantiate(enemyPrefabs[0], spawnPoint.position, Quaternion.identity);
        }
        currentEnemy.SetGameController(this);
        StartCoroutine(ReachEnemy());
    }

    IEnumerator ReachEnemy()
    {
        while (currentEnemy.transform.position.x != 4.5f)
        {
            //currentEnemy.transform.position.x += 0.8f;
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void EnemyDeath(Enemy arg0)
    {
        Debug.LogWarning("OnEnemyDeath");
        currentEnemy = null;
        StartCoroutine(RespawnEnemy(spawnDelay));
    }

    IEnumerator RespawnEnemy(float spawnDelay)
    {
        Debug.LogWarning("Respawn...");
        yield return new WaitForSeconds(spawnDelay);
        SpawnEnemy();
    }
}
