

using System;
using UnityEngine;
using UnityEngine.Serialization;

public class DecisionAttackRange : SMDecision
{
    [Header("Config")]
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask playerMask;

    private EnemyBrain enemy;

    private void Awake()
    {
        enemy = GetComponent<EnemyBrain>();
    }

    public override bool Decide()
    {
        return PlayerInAttackRange();
    }

    private bool PlayerInAttackRange()
    {
        if (enemy.Player == null) return false;
        
        Collider2D playerCollider =
            Physics2D.OverlapCircle(enemy.transform.position, 
                attackRange, playerMask);

        if (playerCollider != null)
        {
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
