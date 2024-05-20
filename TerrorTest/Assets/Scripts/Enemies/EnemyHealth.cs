using System;
using UnityEngine;

/// <summary>
/// Manages the health and damage interactions of an enemy entity.
/// Implements the IDamageable interface to handle taking damage and death.
/// </summary>
public class EnemyHealth : MonoBehaviour, IDamageable
{
    [Header("Enemy Health Configuration")]
    [Tooltip("Initial health of the enemy.")]
    [SerializeField] 
    private float health;
    
    [Header("Customizable")]
    [Tooltip("Show a visual number with the damage the enemy is taking.")]
    public bool showDamageText;

    private readonly int dead = Animator.StringToHash("Dead");
    
    /// <summary>
    /// The current health of the enemy.
    /// </summary>
    public float CurrentHealth { get; set; }

    private Animator animator;
    private EnemyBrain enemyBrain;
    private EnemySelector enemySelector;
    private EnemyLoot enemyLoot;

    /// <summary>
    /// Event triggered when the enemy dies.
    /// </summary>
    public static event Action OnEnemyDeadEvent;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyBrain = GetComponent<EnemyBrain>();
        enemySelector = GetComponent<EnemySelector>();
        enemyLoot = GetComponent<EnemyLoot>();
    }

    private void Start()
    {
        CurrentHealth = health;
    }

    /// <summary>
    /// Inflicts damage to the enemy and checks for death.
    /// </summary>
    /// <param name="amount">The amount of damage to inflict.</param>
    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            DisableEnemy();
        }
        else
        {
            if (!showDamageText) return;
            DamageManager.Instance.ShowDamageText(amount, transform);
        }
    }

    /// <summary>
    /// Disables the enemy upon death and triggers related events and effects.
    /// </summary>
    private void DisableEnemy()
    {
        animator.SetTrigger(dead);
        enemyBrain.enabled = false;
        enemySelector.NoSelectionCallback();
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        OnEnemyDeadEvent?.Invoke();
        GameManager.Instance.AddPlayerExp(enemyLoot.ExpDrop);
    }
}
