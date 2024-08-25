//using Scripts.EnemyScript;
//using Scripts.PlayerScript;
//using System.Collections;
//using UnityEngine;
//using UnityEngine.Events;

//public class OLD_GameController : MonoBehaviour
//{
//    [SerializeField] private Camera mainCamera;  // Камера для определения позиции клика
//    [SerializeField] private Transform spawnPoint;  // Точка спауна врагов
//    [SerializeField] private float spawnDelay;

//    public UnityEvent<Enemy> EnemyDeath = new();

//    public Player player;  // Ссылка на игрока
//    public Enemy[] enemyPrefabs;  // Список префабов врагов
//    private Enemy currentEnemy;  // Текущий враг

//    void Start()
//    {
//        if (currentEnemy == null)
//        {
//            EnemyDeath.AddListener(OnEnemyDeath);
//            SpawnNewEnemy();
//        }
//    }
//    void Update()
//    {
//        //if (Input.GetMouseButtonDown(0)) HandleClick();
//    }

//    //void HandleClick()
//    //{
//    //    // Определите позицию клика мыши
//    //    Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
//    //    RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

//    //    if (hit.collider != null)
//    //    {
//    //        // Проверьте, был ли клик по врагу
//    //        if (hit.collider.TryGetComponent<Enemy>(out var hitEnemy))
//    //        {
//    //            // Нанесите урон врагу
//    //            player.AttackEnemy(hitEnemy);
//    //        }
//    //    }
//    //}

//    public void OnEnemyDeath(Enemy enemy)
//    {
//        Debug.Log("OnEnemyDeath");
//        StartCoroutine(RespawnEnemy(spawnDelay));
//    }

//    void SpawnNewEnemy()
//    {
//        if (enemyPrefabs.Length == 0) return;

//        int enemyChance = Random.Range(0, 101);
//        Debug.Log("enemyChance: " + enemyChance);
//        if (enemyChance <= 20)
//        {
//            Enemy enemyPrefab = enemyPrefabs[^1];
//            if (enemyPrefab != null)
//            {
//                currentEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
//                currentEnemy.GameController = this;
//            }
//        }
//        else
//        {
//            Enemy enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length - 1)];
//            if (enemyPrefab != null)
//            {
//                currentEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
//                currentEnemy.GameController = this;
//            }
//        }
//    }

//    private IEnumerator RespawnEnemy(float delay)
//    {
//        yield return new WaitForSeconds(delay);
//        currentEnemy = null;
//        SpawnNewEnemy();
//    }
//}
