using Scripts.EnemyScript;

namespace Scripts.PlayerScript
{
    public interface IPlayer
    {
        float PlayerDamage { get; set; }
        int CurrentPlayerHealth { get; set; }
        int MaxPlayerHealth { get; set; }
        float PlayerDefence { get; set; }
        //int CurrentPlayerExpirence { get; set; }
        //int MaxPlayerExpirence { get; set; }
       // int PlayerLevel{ get; set; }
    }
}