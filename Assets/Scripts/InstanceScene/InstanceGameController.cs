using Scripts.EnemyScript;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class InstanceGameController : BaseGameController
{
    [SerializeField] private TMP_Text killCountText;
    [SerializeField] private GameObject endRoundCanvas;
    public readonly UnityEvent killLimitReached = new();
    
    private bool isPaused = false;
    private int killCount = 0;
    [SerializeField] private int killLimit = 5;

    protected override void Awake()
    {
        base.Awake();
        killLimitReached.AddListener(EndRound);
        endRoundCanvas.SetActive(false);
        Time.timeScale = isPaused ? 0:1;
        killCountText.text = string.Format("{0}/{1}", killCount, killLimit);
    }

    protected override void HandleEnemyDeath(IEnemy enemy)
    {
        if (!isPaused)
        {
            base.HandleEnemyDeath(enemy);
            killCount++;
            killCountText.text = string.Format("{0}/{1}", killCount, killLimit);
            if (killCount == killLimit)
            {
                killLimitReached.Invoke();
            }
        }
    }

    private void EndRound()
    {
        endRoundCanvas.SetActive(true);
        PauseGame();

    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
    }
}
