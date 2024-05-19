using System;
using UnityEngine;

/// <summary>
/// Manages the health of the player character. Implements the IDamageable interface to handle incoming damage.
/// This component listens for specific key inputs to simulate damage for debugging purposes and updates health accordingly using the PlayerStats ScriptableObject.
/// </summary>
public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Scriptable Objects to Add")]
    [Tooltip("Scriptable Object that contains player statistics")] [SerializeField] 
    private PlayerStats stats;
    
    [Header("Customizable")] [Tooltip("Show a visual number with the damage the player get")]
    public bool showDamageText;

    private float CurrentHealth;
    
    /// Event triggered when the player's health reaches zero.
    public delegate void OnPlayerDeath();
    public event OnPlayerDeath playerDeathEvent;

    private void Start()
    {
        
    }

    private void Update()
    {
        // Only for Debug

        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(1f);
        }
    }

    /// Applies damage to the player's health and triggers death event if health falls to zero or below.
    /// <summary>
    /// <param buttonName="amount">The amount of damage to apply.</param>
    /// </summary>
    public void TakeDamage(float amount)
    {
        if (stats.Health <= 0) return;
        stats.Health -= amount;
        
        if (showDamageText)
        {
            DamageManager.Instance.ShowDamageText(amount, transform);
        }
        
        if (stats.Health <= 0f )
        {
            stats.Health = 0f;
            PlayerDead();    
        }
    }

    public void RestoreHealth(float amount)
    {
        stats.Health += amount;
        if (stats.Health > stats.MaxHealth)
        {
            stats.Health = stats.MaxHealth;
        }
    }

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
