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
        [SerializeField] private int currentPlayerHealth, currentPlayerExpirence;

        public float PlayerDamage { get => playerDamage; set => playerDamage = value; }
        public float PlayerDefence { get => playerDefence; set => playerDefence = value; }
        public int MaxPlayerHealth { get => maxPlayerHealth; set => maxPlayerHealth = value; }
        public int CurrentPlayerHealth { get => currentPlayerHealth; set => currentPlayerHealth = value; }
    }
}
