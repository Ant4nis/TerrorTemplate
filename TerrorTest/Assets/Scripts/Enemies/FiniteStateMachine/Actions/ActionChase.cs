
using System;
using UnityEngine;

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

    public override void Act()
    {
        ChasePlayer();
    }

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
