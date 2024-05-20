using System.Collections;
using UnityEngine;

/// <summary>
/// Manages the temperature of the player character. Implements the IFreezable interface to handle losing temperature.
/// This component updates temperature accordingly using the PlayerStats ScriptableObject.
/// </summary>
public class PlayerTemperature : MonoBehaviour, IFreezable
{
    //public float amountToLoss;

    [Header("Scriptable Object")]
    [Tooltip("Player statistics")] [SerializeField] 
    private PlayerStats stats;

    private Player player;

    /// <summary>
    /// Event triggered when the player's temperature reaches zero.
    /// </summary>
    public delegate void OnPlayerFrozen();
    public event OnPlayerFrozen playerFrozenEvent;

    private Coroutine temperatureCoroutine;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        // Debug: Loss temperature when 'T' key is pressed.
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartLosingTemperature(0.5f);
        }
    }

    /// <summary>
    /// Subtracts temperature from the player's current temperature and triggers the frozen event if temperature falls to zero or below.
    /// </summary>
    /// <param name="amount">The amount of temperature to lose.</param>
    private void LossTemperature(float amount)
    {
        stats.Temperature -= amount;
        if (stats.Temperature <= 0 && !player.IsFrozen)
        {
            stats.Temperature = 0f;
            PlayerFrozen();
        }
    }
    
    /// <summary>
    /// Restores temperature to the player's current temperature up to the maximum temperature.
    /// </summary>
    /// <param name="amount">The amount of temperature to restore.</param>
    public void RestoreTemperature(float amount)
    {
        stats.Temperature += amount;
        if (stats.Temperature > stats.MaxTemperature)
        {
            stats.Temperature = stats.MaxTemperature;
        }
    }
    
    /// <summary>
    /// Starts the coroutine to continuously lose temperature over time.
    /// </summary>
    public void StartLosingTemperature(float amount)
    {
        if (temperatureCoroutine != null) return;
        player.IsSafe = false;
        player.IsUnsafe = true;
        temperatureCoroutine = StartCoroutine(LoseTemperatureOverTime(amount));
    }

    /// <summary>
    /// Stops the coroutine that continuously loses temperature.
    /// </summary>
    public void StopLosingTemperature()
    {
        if (temperatureCoroutine == null) return;
        player.IsUnsafe = false;
        StopCoroutine(temperatureCoroutine);
        temperatureCoroutine = null;
    }
    
    /// <summary>
    /// Starts the coroutine to continuously recovers temperature over time.
    /// </summary>
    public void StartRecoveringTemperature(float amount)
    {
        if (temperatureCoroutine != null) return;
        player.IsSafe = true;
        player.IsUnsafe = false;
        temperatureCoroutine = StartCoroutine(RestoreTemperatureOverTime(amount));
    }

    /// <summary>
    /// Stops the coroutine that continuously recovers temperature.
    /// </summary>
    public void StopRecoveringTemperature()
    {
        if (temperatureCoroutine == null) return;
        player.IsSafe = false;
        player.IsUnsafe = false;
        StopCoroutine(temperatureCoroutine);
        temperatureCoroutine = null;
    }

    /// <summary>
    /// Coroutine to lose temperature over time.
    /// </summary>
    private IEnumerator LoseTemperatureOverTime(float amount)
    {
        while (true)
        {
            //  GetComponent(IFreezable)?.loss
            LossTemperature(amount * Time.deltaTime);
            yield return null;
        }
    }
    
    /// <summary>
    /// Coroutine to restore temperature over time.
    /// </summary>
    private IEnumerator RestoreTemperatureOverTime(float amount)
    {
        while (true)
        {
            //  GetComponent(IFreezable)?.loss
            RestoreTemperature(amount * Time.deltaTime);
            yield return null;
        }
    }

    /// <summary>
    /// Checks if the player's temperature can be restored.
    /// </summary>
    /// <returns>True if the temperature is above zero and below the maximum temperature.</returns>
    public bool CanRestoreTemperature()
    {
        return stats.Temperature > 0 && stats.Temperature < stats.MaxTemperature;
    }

    /// <summary>
    /// Invokes the playerFrozenEvent to notify other components that the player is frozen.
    /// </summary>
    private void PlayerFrozen()
    {
        playerFrozenEvent?.Invoke();
    }

    
}
