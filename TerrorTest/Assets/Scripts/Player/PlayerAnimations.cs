using System;
using UnityEngine;

/// <summary>
/// Manages the player's animations based on movement and health state changes.
/// This script interacts with the Animator to control animations like walking, running, and dying,
/// based on input from PlayerMovement for motion changes and PlayerHealth for health status changes.
/// It ensures that the visual representation of the player's actions is synchronized with the game mechanics.
/// </summary>
public class PlayerAnimations : MonoBehaviour
{
    [Header("Layers Configuration")]
    [Tooltip("Name of the Animator layer for the Idle state")]
    [SerializeField]
    private string layerIdle;

    [Tooltip("Name of the Animator layer for the dead and frozen state")]
    [SerializeField]
    private string layerDead;

    private readonly int xDirection = Animator.StringToHash("X"); // Guarda el ID como variable
    private readonly int yDirection = Animator.StringToHash("Y");
    private readonly int isWalking = Animator.StringToHash("IsWalking");
    private readonly int isRunning = Animator.StringToHash("IsRunning");
    private readonly int isAttacking = Animator.StringToHash("IsAttacking");
    private readonly int idle = Animator.StringToHash("Idle");
    private readonly int dead = Animator.StringToHash("Dead");
    private readonly int frozen = Animator.StringToHash("Frozen");
    private readonly int revive = Animator.StringToHash("Revive");

    private int lastActivatedLayer = -1;

    private Animator animator;
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;
    private PlayerTemperature playerTemperature;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<PlayerHealth>();
        playerTemperature = GetComponent<PlayerTemperature>();
    }

    /// <summary>
    /// Activates the specified Animator layer and plays the given animation cycle.
    /// </summary>
    /// <param name="layerName">The name of the Animator layer to activate.</param>
    /// <param name="cycle">The animation cycle to play.</param>
    private void ActivateLayer(string layerName, int cycle)
    {
        int layerIndex = animator.GetLayerIndex(layerName);

        // Verificamos si la capa ya está activa para evitar reiniciar la animación
        if (lastActivatedLayer != layerIndex)
        {
            for (int i = 0; i < animator.layerCount; i++)
            {
                animator.SetLayerWeight(i, 0);
            }

            animator.SetLayerWeight(layerIndex, 1);

            // Reiniciamos la animación solo si la capa actual difiere de la última activada
            animator.Play(cycle, layerIndex, 0);

            // Actualizamos la última capa activada
            lastActivatedLayer = layerIndex;
        }
    }

    /// <summary>
    /// Sets the appropriate movement animation based on the player's current speed.
    /// This method determines whether the player is running or walking and sets the corresponding Animator flags.
    /// </summary>
    /// <param name="value">Whether the player is moving or not.</param>
    /// <param name="currentSpeed">The current speed of the player.</param>
    public void SetMovingTypeTransition(bool value, float currentSpeed)
    {
        if (Math.Abs(currentSpeed - playerMovement.RunningSpeed) < Mathf.Epsilon)
        {
            animator.SetBool(isRunning, value);
            animator.SetBool(isWalking, false);
        }
        else
        {
            animator.SetBool(isWalking, value);
            animator.SetBool(isRunning, false);
        }
    }

    /// <summary>
    /// Updates the movement direction in the Animator based on the player's current direction.
    /// This is used to adjust the animation parameters for directional movement.
    /// </summary>
    /// <param name="dir">The current direction vector of the player.</param>
    public void SetMoveAnimation(Vector2 dir)
    {
        animator.SetFloat(xDirection, dir.x);
        animator.SetFloat(yDirection, dir.y);
    }

    /// <summary>
    /// Sets the attack animation trigger in the Animator.
    /// </summary>
    /// <param name="value">Whether the player is attacking or not.</param>
    public void SetAttackAnimation(bool value)
    {
        animator.SetBool(isAttacking, value);
    }

    /// <summary>
    /// Sets the dead animation trigger in the Animator.
    /// This method is typically called when the player's health reaches zero.
    /// </summary>
    private void SetDeadAnimation()
    {
        ActivateLayer(layerDead, dead);
    }

    /// <summary>
    /// Sets the frozen animation trigger in the Animator.
    /// This method is typically called when the player is frozen.
    /// </summary>
    private void SetFrozenAnimation()
    {
        ActivateLayer(layerDead, frozen);
    }

    /// <summary>
    /// Resets the player's animation to the idle state.
    /// This method is called when the player is revived.
    /// </summary>
    public void ResetPlayer()
    {
        SetMoveAnimation(Vector2.down); // Para mostrar la animación mirando hacia abajo
        ActivateLayer(layerIdle, idle);
    }

    /// <summary>
    /// Registers the SetDeadAnimation method to the playerDeathEvent event and SetFrozenAnimation method to the playerFrozenEvent.
    /// This ensures the animation is triggered when the player dies or is frozen.
    /// </summary>
    private void OnEnable()
    {
        playerHealth.playerDeathEvent += SetDeadAnimation;
        playerTemperature.playerFrozenEvent += SetFrozenAnimation;
    }

    /// <summary>
    /// Unregisters the SetDeadAnimation method from the playerDeathEvent event.
    /// This is necessary to clean up references when the script is disabled.
    /// </summary>
    private void OnDisable()
    {
        playerHealth.playerDeathEvent -= SetDeadAnimation;
        playerTemperature.playerFrozenEvent -= SetFrozenAnimation;
    }
}
