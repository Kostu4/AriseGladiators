using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Scripts.EnemyScript;
using Scripts.PlayerScript;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

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
        [SerializeField] private int killCount = 0;

        protected override void Awake()
        {
            base.Awake();
            timer.TimeOut.AddListener(TimerIsOut);
            endRoundCanvas.SetActive(false);
            Time.timeScale = isPaused ? 0 : 1;
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

        private void PauseGame()
        {
            isPaused = true;
        }

        private void ResumeGame()
        {
            isPaused = false;
        }
    }
}

