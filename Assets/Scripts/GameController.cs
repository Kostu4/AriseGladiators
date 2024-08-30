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
    [SerializeField] Transform spawnEnemyPoint;
    [SerializeField] Transform spawnPlayerPoint;
    [SerializeField] private float spawnDelay;
    public UnityEvent<Enemy> isEnemyDead = new();

    private void Awake()
    {
        if (playerPrefab != null)
        { 
            Instantiate(playerPrefab, spawnPlayerPoint.position, Quaternion.identity);
        }
        if (enemyPrefabs != null)
        {
            SpawnEnemy();
        }
        isEnemyDead.AddListener(EnemyDeath);
    }

    private void SpawnEnemy()
    {
        int enemyChance = UnityEngine.Random.Range(0, 101);
        if (enemyChance <= 12)
        {
            currentEnemy = Instantiate(enemyPrefabs[1], spawnEnemyPoint.position, Quaternion.identity);
        }
        else
        {
            currentEnemy = Instantiate(enemyPrefabs[0], spawnEnemyPoint.position, Quaternion.identity);
        }
        currentEnemy.SetGameController(this);
    }

    public void EnemyDeath(Enemy arg0)
    {
        currentEnemy = null;
        StartCoroutine(RespawnEnemy(spawnDelay));
    }

    IEnumerator RespawnEnemy(float spawnDelay)
    {
        yield return new WaitForSeconds(spawnDelay);
        SpawnEnemy();
    }
}
