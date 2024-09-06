using Scripts.EnemyScript;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class InstanceGameController : BaseGameController
{
    [SerializeField] private TMP_Text killCountText;
    [SerializeField] private GameObject endRoundCanvas;
    public readonly UnityEvent killLimitReached = new();

    private int killCount = 0;
    [SerializeField] private int killLimit = 5;

    protected override void Awake()
    {
        base.Awake();
        killLimitReached.AddListener(EndRound);
        endRoundCanvas.SetActive(false);
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
            CoinsController.Instance.AddCoins(enemy.EnemyCoins);
        }
    }
    private void EndRound()
    {
        endRoundCanvas.SetActive(true);
        PauseGame();
        CoinsController.Instance.SaveCoins();
    }
}