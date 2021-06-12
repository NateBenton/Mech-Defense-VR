namespace _NBGames.Scripts.Interfaces
{
    public interface IDamageable
    {
        float Health { get; set; }
        void TakeDamage(float damageAmount);
    }
}