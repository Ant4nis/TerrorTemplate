using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Decision to determine if the player is detected within a certain range.
/// </summary>
public class DecisionDetectPlayer : SMDecision
{
    [Header("Config")]
    [Tooltip("The detection range of the enemy.")]
    [SerializeField] private float range;
    
    [Tooltip("Layer mask to identify the player.")]
    [SerializeField] private LayerMask playerMask;

    private EnemyBrain enemy;

    private void Awake()
    {
        enemy = GetComponent<EnemyBrain>();
    }

    /// <summary>
    /// Decides whether the player is detected within range.
    /// </summary>
    /// <returns>True if the player is detected, false otherwise.</returns>
    public override bool Decide()
    {
        return DetectPlayer();
    }

    /// <summary>
    /// Checks if the player is within the detection range.
    /// </summary>
    /// <returns>True if the player is detected, false otherwise.</returns>
    private bool DetectPlayer()
    {
        Collider2D playerCollider =
            Physics2D.OverlapCircle(enemy.transform.position, range, playerMask);

        if (playerCollider != null)
        {
            enemy.Player = playerCollider.transform;
            return true;
        }

        enemy.Player = null;
        return false;
    }

    /// <summary>
    /// Draws a gizmo in the Unity editor to visualize the detection range.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}