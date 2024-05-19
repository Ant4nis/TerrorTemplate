 using System;
 using UnityEngine;

 public class EnemyHealth : MonoBehaviour, IDamageable
 {    
     [Header("Enemy Health Configuration")]
     [Tooltip("Scriptable Object that contains player statistics")] [SerializeField] 
     private float health;
     [Header("Customizable")] 
     [Tooltip("Show a visual number with the damage the enemy is getting")]
     public bool showDamageText;
     
     private readonly int dead = Animator.StringToHash("Dead");
     public float CurrentHealth { get; set; }
     
     private Animator animator;
     private EnemyBrain enemyBrain;
     private EnemySelector enemySelector;
     private EnemyLoot enemyLoot;
     
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
     
     public void TakeDamage(float amount)
     {
         CurrentHealth -= amount;
         if (CurrentHealth <= 0)
         {
             DisableEnemy();
         }
         else
         {
             if (showDamageText == false) return;
             DamageManager.Instance.ShowDamageText(amount, transform);
         }
     }

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
