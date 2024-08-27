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
        int enemyChance = UnityEngine.Random.Range(0, 101);
        Debug.Log("enemyChance: " + enemyChance);
        if (enemyChance <= 12)
        {
            currentEnemy = Instantiate(enemyPrefabs[1], spawnPoint.position, Quaternion.identity);
        }
        else
        {
            currentEnemy = Instantiate(enemyPrefabs[0], spawnPoint.position, Quaternion.identity);
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
