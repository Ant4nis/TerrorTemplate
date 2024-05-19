using System;
using UnityEngine;

public class ActionAttack : SMAction
{
    [Header("Attack Configuration")]
    [Tooltip("Damage of the enemy")]
    [SerializeField] private float damage; // The damage force of the entity.
    [Tooltip("The time to wait until the next attack")]
    [SerializeField] private float timeBtwAttacks; // The damage force of the entity.

    private float timer;

    private EnemyBrain enemyBrain;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }


    public override void Act()
    {
        AttackPlayer();
    }

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
