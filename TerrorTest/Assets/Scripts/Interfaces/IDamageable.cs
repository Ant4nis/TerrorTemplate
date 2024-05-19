/// <summary>
/// Defines a damageable entity in the game.
/// Any object that implements this interface can be damaged and must define the TakeDamage method.
/// </summary>
public interface IDamageable
{
    void TakeDamage(float amount);
}
