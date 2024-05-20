using System;
using UnityEngine;

/// <summary>
/// The ActionAttack class handles the attack behavior of an enemy entity.
/// It periodically damages the player if within range.
/// </summary>
public class ActionAttack : SMAction
{
    [Header("Attack Configuration")]
    [Tooltip("Damage of the enemy")]
    [SerializeField] private float damage; // The damage dealt by the entity.
    
    [Tooltip("The time to wait until the next attack")]
    [SerializeField] private float timeBtwAttacks; // The time interval between attacks.

    private float timer;
    private EnemyBrain enemyBrain;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }

    /// <summary>
    /// Executes the attack action.
    /// </summary>
    public override void Act()
    {
        AttackPlayer();
    }

    /// <summary>
    /// Attacks the player if within range and the cooldown timer has elapsed.
    /// </summary>
    private void AttackPlayer()
    {
        if (enemyBrain.Player == null) return;
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            IDamageable player = enemyBrain.Player.GetComponent<IDamageable>();
            player.TakeDamage(damage);
            timer = timeBtwAttacks;
        }
    }
}