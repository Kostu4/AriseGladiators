using UnityEngine;
using Scripts.EnemyScript;

namespace Scripts.PlayerScript
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int playerDamage;
        [SerializeField] private int maxPlayerHealth;
        [SerializeField] private float playerDefence;

        public int PlayerDamage { get => playerDamage; set => playerDamage = value; }
        public float PlayerDefence { get => playerDefence; set => playerDefence = value; }
        public int MaxPlayerHealth { get => maxPlayerHealth; set => maxPlayerHealth = value; }
        public int CurrentPlayerHealth { get ; set ; }
    }
}
