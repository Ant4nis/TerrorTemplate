using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PlayerAttack : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("If the player shows his equiped weapon or light, if is empty, will not show weapon")] [SerializeField]
    private ShowObjectInHand showObjectInHand;
    [Tooltip("If the player can attack with distance weapons")] [SerializeField] 
    private bool canDistanceAttack;
    [Tooltip("If the player can only attack if there is an enemy selected")] [SerializeField] 
    private bool onlyAttackWithSelection;
    [Tooltip("If the has a initial weapon")] //[SerializeField]
    public bool startWeapon;
    
    [Header("Attack Configuration")]
    [Tooltip("Player's initial weapon")] [SerializeField] 
    private PlayerStats stats;
    [Tooltip("Player's initial weapon")] [SerializeField] 
    private LayerMask layerToHit;
    [Tooltip("Player's initial weapon")] [SerializeField] 
    private Weapon initialWeapon;
    [Tooltip("Array with player's attack position")] [SerializeField] 
    private Transform[] attackPositions;
    
    [Header("Melee Configuration")]
    [Tooltip("If the player can attack with melee weapons")] [SerializeField] 
    private bool canMeleeAttack;
    [Tooltip("The distace between enemy and player to hit")] [SerializeField] 
    private float minDistanceMelee;
    [Tooltip("The distace between enemy and player to hit")] [SerializeField] 
    private float attackRadius = 0.5f;
    [Tooltip("If the player can attack with melee weapons")] [SerializeField]
    private ParticleSystem slashFX;


    public string id { get; set; }
    public Weapon CurrentWeapon { get; set; }
    
    private PlayerActions actions;
    private Player player;
    private PlayerAnimations playerAnimations;
    private PlayerMovement playerMovement;
    private EnemyBrain enemyTarget;
    private PlayerAmmo playerAmmo;
    private Coroutine attackCoroutine;

    private Transform currentAttackPosition;
    private float currentAttackRotation;

    private void Awake()
    {
        actions = new PlayerActions();
        playerAmmo = GetComponent<PlayerAmmo>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimations = GetComponent<PlayerAnimations>();
        player = GetComponent<Player>();
    }

    private void Start()
    { 
        if (startWeapon)
        {
            id = initialWeapon.IdToEquipAtStart;
            EquipWeapon(initialWeapon);
        }        
        actions.Attack.Attack.performed += ctx => Attack();
    }

    private void Update()
    {
        
        GetFirePosition();
    }

    /// <summary>
    /// Initiates the attack process if conditions are met.
    /// </summary>
    private void Attack()
    {
        if (player.IsAttacking || player.IsDead || player.IsFrozen || CurrentWeapon == null) return;
        if (!canMeleeAttack && !canDistanceAttack) return;
        if (onlyAttackWithSelection && enemyTarget == null) return;
        if (currentAttackPosition == null || CurrentWeapon == null) return;
        
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }

        attackCoroutine = StartCoroutine(IEAttack());
    }

    /// <summary>
    /// Handles the attack coroutine to manage attack animations and logic.
    /// </summary>
    private IEnumerator IEAttack()
    {
        if (currentAttackPosition == null || CurrentWeapon == null)
        {
            yield break;
        }

        player.IsAttacking = true;

        if (CurrentWeapon.WeaponType == WeaponType.Distance)
        {
            if (playerAmmo.currentAmmo < CurrentWeapon.RequiredAmmo)
            {
                player.IsAttacking = false;
                yield break;
            }
            if (canDistanceAttack == false) yield break;
            DistanceAttack();
        }
        else
        {
            if (canMeleeAttack == false) yield break;
            MeleeAttack();
        }

        playerAnimations.SetAttackAnimation(true);
        yield return new WaitForSeconds(0.5f);
        playerAnimations.SetAttackAnimation(false);
        player.IsAttacking = false;
    }

    /// <summary>
    /// Executes a distance attack.
    /// </summary>
    private void DistanceAttack()
    {
        Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, currentAttackRotation));
        GameObject projectileObject = ObjectPoolManager.Instance.SpawnFromPool("Arrow", currentAttackPosition.position, rotation);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Direction = Vector3.up; // Configured for turn projectiles facing up
        projectile.Damage = GetAttackDamage();
        playerAmmo.UseAmmo(CurrentWeapon.RequiredAmmo);
    }
    
    /// <summary>
    /// Executes a melee attack.
    /// </summary>
    private void MeleeAttack()
    {
        if (slashFX != null)
        {
            slashFX.transform.position = currentAttackPosition.position;
            slashFX.Play();
        }

        Vector2 position = currentAttackPosition.position;
        
        Collider2D[] hits = Physics2D.OverlapCircleAll(position, attackRadius, layerToHit);

        foreach (var hit in hits)
        {
            IDamageable damageable = hit.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(GetAttackDamage());
            }
        }
    }

    /// <summary>
    /// Changue a weapon and show it in game
    /// </summary>
    /// <param name="newWeapon"> The weapon associated to equip</param>
    public void EquipWeapon(Weapon newWeapon)
    {
        CurrentWeapon = newWeapon;
        stats.TotalDamage = stats.BaseDamage + CurrentWeapon.Damage;

       // if (showObjectInHand == null) return;
        
        Debug.Log(id);
        showObjectInHand.ActivateWeapon(id);
    }
    
    /// <summary>
    /// Calculates the to total damage the player can do.
    /// </summary>
    /// <returns>total damage</returns>
    private float GetAttackDamage()
    {
        float damage = stats.BaseDamage;
        damage += CurrentWeapon.Damage;
        float randomPerc = Random.Range(0f, 100f);
        
        if (randomPerc <= stats.CriticalChance)
        {
            damage += damage * (stats.CriticalDamage / 100);
        }

        return damage;
    }
    
    /// <summary>
    /// Calculates the different positions to attack depending on move direction
    /// </summary>
    private void GetFirePosition()
    {
        Vector2 moveDirection = playerMovement.MoveDirection;
        
        // Normalizar el vector de dirección para manejar correctamente las diagonales
        moveDirection.Normalize();

        switch (moveDirection)
        {
            // Arriba
            case Vector2 dir when dir == Vector2.up:
                currentAttackPosition = attackPositions[0];
                currentAttackRotation = 0f;
                break;
            // Abajo
            case Vector2 dir when dir == Vector2.down:
                currentAttackPosition = attackPositions[1];
                currentAttackRotation = -180f;
                break;
            // Izquierda
            case Vector2 dir when dir == Vector2.left:
                currentAttackPosition = attackPositions[2];
                currentAttackRotation = -270f;
                break;
            // Derecha
            case Vector2 dir when dir == Vector2.right:
                currentAttackPosition = attackPositions[3];
                currentAttackRotation = -90f;
                break;
            // Arriba-Izquierda
            case Vector2 dir when dir == new Vector2(-1, 1).normalized:
                currentAttackPosition = attackPositions[4];
                currentAttackRotation = -315f;
                break;
            // Arriba-Derecha
            case Vector2 dir when dir == new Vector2(1, 1).normalized:
                currentAttackPosition = attackPositions[5];
                currentAttackRotation = -45f;
                break;
            
            // Abajo-Izquierda
            case Vector2 dir when dir == new Vector2(-1, -1).normalized:
                currentAttackPosition = attackPositions[6];
                currentAttackRotation = -225f;
                break;

            // Abajo-Derecha
            case Vector2 dir when dir == new Vector2(1, -1).normalized:
                currentAttackPosition = attackPositions[7];
                currentAttackRotation = -135f;
                break;
        }
    }
    
    private void OnDrawGizmos()
    {
        if (currentAttackPosition != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(currentAttackPosition.position, attackRadius);
        }
    }
    
    /// <summary>
    /// If you want to attack only if there is an enemy selected, this metod is waiting for an event.
    /// </summary>
    /// <param name="enemySelected">the enemy you clciked</param>
    private void EnemySelectedCallback(EnemyBrain enemySelected)
    {
        enemyTarget = enemySelected;
    }
    
    /// <summary>
    /// Deletes the enemy selection
    /// </summary>
    private void NoSelectionCallback()
    {
        enemyTarget = null;
    }

    private void OnEnable()
    {
        actions.Enable();
        SelectionManager.OnEnemySelectedEvent += EnemySelectedCallback;
        SelectionManager.OnNullSelectionEvent += NoSelectionCallback;
        EnemyHealth.OnEnemyDeadEvent += NoSelectionCallback;
    }

    private void OnDisable()
    {
        actions.Disable();
        SelectionManager.OnEnemySelectedEvent -= EnemySelectedCallback;
        SelectionManager.OnNullSelectionEvent -= NoSelectionCallback;
        EnemyHealth.OnEnemyDeadEvent -= NoSelectionCallback;

    }
}
