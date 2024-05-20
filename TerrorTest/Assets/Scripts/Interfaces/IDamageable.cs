/// <summary>
/// Defines a damageable entity in the game.
/// Any object that implements this interface can be damaged and must define the TakeDamage method.
/// </summary>
public interface IDamageable
{
    /// <summary>
    /// Inflicts damage to the entity.
    /// </summary>
    /// <param name="amount">The amount of damage to inflict.</param>
    void TakeDamage(float amount);
}