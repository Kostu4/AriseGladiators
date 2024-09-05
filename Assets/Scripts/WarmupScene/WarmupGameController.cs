using TMPro;
using UnityEngine;
using Scripts.EnemyScript;


namespace Scripts.WarmupScene
{ 
    public class WarmupGameController : BaseGameController
    {
        [SerializeField] private Timer timer;
        [SerializeField] GameObject endRoundCanvas;
        [SerializeField] TMP_Text killCountText;
        [SerializeField] TMP_Text coinsCollectedText;
        
        private int coinsCollected = 0;
        [SerializeField] private int killCount = 0;

        protected override void Awake()
        {
            base.Awake();
            timer.TimeOut.AddListener(TimerIsOut);
            endRoundCanvas.SetActive(false);
        }

        
        protected override void HandleEnemyDeath(IEnemy enemy)
        {
            base.HandleEnemyDeath(enemy);
            killCount++;
            killCountText.text = killCount.ToString();
            coinsCollected += enemy.EnemyCoins*2;
            coinsCollectedText.text = coinsCollected.ToString();
            GameManager.Instance.AddCoins(coinsCollected);
        }

        private void TimerIsOut()
        {
            endRoundCanvas.SetActive(true);
            PauseGame();
            GameManager.Instance.SaveCoins();
        }
    }
}

