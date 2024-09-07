//using Scripts.EnemyScript;
//using System.Collections;
//using TMPro;
//using UnityEngine;
//using UnityEngine.Events;

//public class InstanceGameController : BaseGameController
//{
//    [SerializeField] private TMP_Text killCountText;
//    [SerializeField] private GameObject endRoundCanvas;
//    public readonly UnityEvent killLimitReached = new();

//    public int KillCount { get; set; }
//    public int KillLimit { get; set; }
//    public bool isReadyToStart = false;

//    protected override void Awake()
//    {
//        base.Awake();
//        killLimitReached.AddListener(EndRound);
//        endRoundCanvas.SetActive(false);
//        killCountText.text = string.Format("{0}/{1}", KillCount=LevelManager.Instance.enemiesKilled, KillLimit = LevelManager.Instance.enemiesToKill);
//    }

//    protected override void HandleEnemyDeath(IEnemy enemy)
//    {
//        if (!isPaused)
//        {
//            LevelManager.Instance.OnEnemyKilled();
//            killCountText.text = string.Format("{0}/{1}", KillCount = LevelManager.Instance.enemiesKilled, KillLimit = LevelManager.Instance.enemiesToKill);

            
//            base.HandleEnemyDeath(enemy);
//        }
//    }
//    public void EndRound()
//    {
//        endRoundCanvas.SetActive(true);
//        PauseGame();
//        StartCoroutine(ChangeScene());
//    }
//    public void ReadyToNextLevel()
//    {
//        isReadyToStart = true;
//    }
//    IEnumerator ChangeScene()
//    {
//        while(isReadyToStart != true)
//        {
//            yield return null;
//        }
//        LevelManager.Instance.ReloadScene();
        
//    }
//}