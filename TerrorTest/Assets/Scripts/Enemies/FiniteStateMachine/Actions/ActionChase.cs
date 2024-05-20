using System;
using UnityEngine;

/// <summary>
/// The ActionChase class handles the chasing behavior of an enemy entity.
/// It moves towards the player if the player is within a certain distance.
/// </summary>
public class ActionChase : SMAction
{
    [Header("Movement Configuration")]
    [Tooltip("Enemy Speed")]
    [SerializeField] private float chaseSpeed; // The movement speed of the entity.

    private EnemyBrain enemyBrain;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }

    /// <summary>
    /// Executes the chase action.
    /// </summary>
    public override void Act()
    {
        ChasePlayer();
    }

    /// <summary>
    /// Moves the entity towards the player if the player is within a certain distance.
    /// </summary>
    private void ChasePlayer()
    {
        if (enemyBrain.Player == null) return;

        Vector3 dirToPlayer = enemyBrain.Player.position - transform.position;

        if (dirToPlayer.magnitude >= 1.3f)
        {
            transform.Translate(dirToPlayer.normalized * (chaseSpeed * Time.deltaTime));
        }
    }
}