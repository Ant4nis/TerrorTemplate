using System;
using UnityEngine;

/// <summary>
/// Manages the health of the player character. Implements the IDamageable interface to handle incoming damage.
/// This component listens for specific key inputs to simulate damage for debugging purposes and updates health accordingly using the PlayerStats ScriptableObject.
/// </summary>
public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Scriptable Objects to Add")]
    [Tooltip("Scriptable Object that contains player statistics")]
    [SerializeField] 
    private PlayerStats stats;

    [Header("Customizable")]
    [Tooltip("Show a visual number with the damage the player gets")]
    public bool showDamageText;

    private float currentHealth;

    /// <summary>
    /// Event triggered when the player's health reaches zero.
    /// </summary>
    public delegate void OnPlayerDeath();
    public event OnPlayerDeath playerDeathEvent;

    private void Start()
    {
        currentHealth = stats.Health;
    }

    private void Update()
    {
        // Only for Debug
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(1f);
        }
    }

    /// <summary>
    /// Applies damage to the player's health and triggers death event if health falls to zero or below.
    /// </summary>
    /// <param name="amount">The amount of damage to apply.</param>
    public void TakeDamage(float amount)
    {
        if (stats.Health <= 0) return;
        stats.Health -= amount;

        if (showDamageText)
        {
            DamageManager.Instance.ShowDamageText(amount, transform);
        }

        if (stats.Health <= 0f)
        {
            stats.Health = 0f;
            PlayerDead();
        }
    }

    /// <summary>
    /// Restores health to the player up to the maximum health limit.
    /// </summary>
    /// <param name="amount">The amount of health to restore.</param>
    public void RestoreHealth(float amount)
    {
        stats.Health += amount;
        if (stats.Health > stats.MaxHealth)
        {
            stats.Health = stats.MaxHealth;
        }
    }

    /// <summary>
    /// Checks if the player's health can be restored.
    /// </summary>
    /// <returns>True if health can be restored, false otherwise.</returns>
    public bool CanRestoreHealth()
    {
        return stats.Health > 0 && stats.Health < stats.MaxHealth;
    }

    /// <summary>
    /// Invokes the playerDeathEvent to notify other components of the player's death.
    /// </summary>
    private void PlayerDead()
    {
        playerDeathEvent?.Invoke();
    }
}
