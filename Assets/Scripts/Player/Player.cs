using UnityEngine;
using Scripts.EnemyScript;

namespace Scripts.PlayerScript
{
    public class Player : MonoBehaviour, IPlayer
    {
        [SerializeField] private float playerDamage;
        [SerializeField] private int maxPlayerExpirence;
        [SerializeField] private int maxPlayerHealth;
        [SerializeField] private float playerDefence;
        [SerializeField] private int currentPlayerHealth, currentPlayerExpirence; //playerLevel =1;

        public float PlayerDamage { get => playerDamage; set => playerDamage = value; }
        public float PlayerDefence { get => playerDefence; set => playerDefence = value; }
        public int MaxPlayerHealth { get => maxPlayerHealth; set => maxPlayerHealth = value; }
        //public int MaxPlayerExpirence { get => maxPlayerExpirence; set => maxPlayerExpirence = value; }
        public int CurrentPlayerHealth { get => currentPlayerHealth; set => currentPlayerHealth = value; }
        //public int CurrentPlayerExpirence { get => currentPlayerExpirence; set => currentPlayerExpirence = value; }
        //public int PlayerLevel { get => playerLevel; set => playerLevel = value; }

        //public void LevelUp()
        //{
        //        maxPlayerHealth += (int)(maxPlayerHealth * 0.1f);
        //        maxPlayerExpirence += 100;
        //        playerLevel++;
        //        currentPlayerHealth = maxPlayerHealth;
        //        currentPlayerExpirence = 0;
        //}
        //public void GainExpirence()
        //{
        //    ExpirenceController.Instance.TransExpirence(currentPlayerExpirence);
        //    if (currentPlayerExpirence >= maxPlayerExpirence) LevelUp();
        //}
    }
}
