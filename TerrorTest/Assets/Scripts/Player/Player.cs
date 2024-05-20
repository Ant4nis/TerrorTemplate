using System;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Represents the player character in the game.
/// This class manages the access to the player's statistics through a Scriptable Object reference.
/// </summary>
public class Player : MonoBehaviour
{
    [Header("test")] 
    public ItemHealthKit HealthKit;
    public ItemHotPotion HotPotion;
    public ItemTrankimazin Trankimazin;
    public ItemSkillBook SkillBook;

    [Header("Reviving Player")]
    [Tooltip("If true, when reviving player all stats set to initial values, if false, only restores health, madness and temperature")]
    [SerializeField] 
    private bool resetAllStats;

    [Header("Customizable Details")]
    [Tooltip("Stop moving when a wall is in front of")]
    public bool stopAtWall;

    [Tooltip("Layer masks to detect as wall")]
    public LayerMask wallMask;

    [Header("Scriptable Object to Add")]
    [Tooltip("Scriptable Object containing player statistics")]
    [SerializeField] 
    private PlayerStats stats;

    private bool isDead; 
    private bool isFrozen;

    public bool IsSafe { get; set; }
    public bool IsUnsafe { get; set; }
    public bool IsDead => isDead;
    public bool IsFrozen => isFrozen;
    public PlayerAmmo PlayerAmmo { get; private set; }
    public PlayerHealth PlayerHealth { get; private set; }
    public PlayerTemperature PlayerTemperature { get; private set; }
    public PlayerMadness PlayerMadness { get; private set; }
    public PlayerAttack PlayerAttack { get; private set; }
    public PlayerStats Stats => stats;
    public bool IsAttacking { get; set; }

    private PlayerAnimations animations;

    private void Awake()
    {
        PlayerHealth = GetComponent<PlayerHealth>();
        PlayerTemperature = GetComponent<PlayerTemperature>();
        PlayerMadness = GetComponent<PlayerMadness>();
        PlayerAttack = GetComponent<PlayerAttack>();
        PlayerAmmo = GetComponent<PlayerAmmo>();
        animations = GetComponent<PlayerAnimations>();
    }

    /// <summary>
    /// Sets the player state to dead and stops any attacking actions.
    /// </summary>
    private void SetDeadPlayer()
    {
        isDead = true;
        IsAttacking = false;
    }

    /// <summary>
    /// Sets the player state to frozen and stops any attacking actions.
    /// </summary>
    private void SetFrozenPlayer()
    {
        isFrozen = true;
        IsAttacking = false;
    }

    /// <summary>
    /// Revives the player, restoring health and other stats based on the resetAllStats flag.
    /// </summary>
    public void RevivePlayer()
    {
        isDead = false;
        isFrozen = false;

        if (resetAllStats)
        {
            stats.ResetStats();
            animations.ResetPlayer();
            PlayerAmmo.ReloadAmmo();
            return;
        }

        stats.MinimumResetStats();
        animations.ResetPlayer();
        PlayerAmmo.ReloadAmmo();
    }

    /// <summary>
    /// Registers the SetDeadPlayer and SetFrozenPlayer methods to the playerDeathEvent and playerFrozenEvent events.
    /// This ensures the appropriate state is set when the player dies or gets frozen.
    /// </summary>
    private void OnEnable()
    {
        PlayerHealth.playerDeathEvent += SetDeadPlayer;
        PlayerTemperature.playerFrozenEvent += SetFrozenPlayer;
    }

    /// <summary>
    /// Unregisters the SetDeadPlayer and SetFrozenPlayer methods from the playerDeathEvent and playerFrozenEvent events.
    /// This is necessary to clean up references when the script is disabled.
    /// </summary>
    private void OnDisable()
    {
        PlayerHealth.playerDeathEvent -= SetDeadPlayer;
        PlayerTemperature.playerFrozenEvent -= SetFrozenPlayer;
    }
}
