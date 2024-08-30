using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.WarmupScene
{ 
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Timer timer;
        private void Awake()
        {
            timer.TimeOut.AddListener(TimerIsOut);
        }

        private void TimerIsOut()
        {
            
        }
    }
}

