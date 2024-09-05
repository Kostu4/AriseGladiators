using TMPro;
using UnityEngine;
using Scripts.EnemyScript;
using UnityEngine.UI;

namespace Scripts.WarmupScene
{ 
    public class WarmupGameController : BaseGameController
    {
        [SerializeField] private Timer timer;
        [SerializeField] GameObject endRoundCanvas;
        [SerializeField] TMP_Text killCountText;
        [SerializeField] TMP_Text coinsCollectedText;
        
        private bool isPaused = false;
        private int coinsCollected = 0;
        private int killCount = 0;

        protected override void Awake()
        {
            base.Awake();
            timer.TimeOut.AddListener(TimerIsOut);
            endRoundCanvas.SetActive(false);
            Time.timeScale = isPaused ? 0 : 1;
        }

        protected override void HandleEnemyDeath(IEnemy enemy)
        {
            if (!isPaused)
            { 
                base.HandleEnemyDeath(enemy);
                killCount++;
                killCountText.text = killCount.ToString();
                coinsCollected += enemy.EnemyCoins/2;
                coinsCollectedText.text = coinsCollected.ToString();
            }
        }

        private void TimerIsOut()
        {
            endRoundCanvas.SetActive(true);
            PauseGame();
            PlayerPrefs.SetInt("coins", coinsCollected);
        }

        private void PauseGame()
        {
            isPaused = true;
            Time.timeScale = 0;
        }

        private void ResumeGame()
        {
            isPaused = false;
            Time.timeScale = 1;
        }
    }
}

