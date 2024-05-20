using System;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Decision to determine if the player is within the attack range of the enemy.
/// </summary>
public class DecisionAttackRange : SMDecision
{
    [Header("Config")]
    [Tooltip("The attack range of the enemy.")]
    [SerializeField] private float attackRange;
    
    [Tooltip("Layer mask to identify the player.")]
    [SerializeField] private LayerMask playerMask;

    private EnemyBrain enemy;

    private void Awake()
    {
        enemy = GetComponent<EnemyBrain>();
    }

    /// <summary>
    /// Decides whether the player is within the attack range.
    /// </summary>
    /// <returns>True if the player is within attack range, false otherwise.</returns>
    public override bool Decide()
    {
        return PlayerInAttackRange();
    }

    /// <summary>
    /// Checks if the player is within the attack range.
    /// </summary>
    /// <returns>True if the player is within attack range, false otherwise.</returns>
    private bool PlayerInAttackRange()
    {
        if (enemy.Player == null) return false;
        
        Collider2D playerCollider =
            Physics2D.OverlapCircle(enemy.transform.position, attackRange, playerMask);

        return playerCollider != null;
    }

    /// <summary>
    /// Draws a gizmo in the Unity editor to visualize the attack range.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}