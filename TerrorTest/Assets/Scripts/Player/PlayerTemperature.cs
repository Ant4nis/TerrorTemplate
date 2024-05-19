using System.Collections;
using UnityEngine;

/// <summary>
/// Manages the temperature of the player character. Implements the IFreezable interface to handle losing temperature.
/// This component updates temperature accordingly using the PlayerStats ScriptableObject.
/// </summary>
public class PlayerTemperature : MonoBehaviour, IFreezable
{
    public float amountToLoss;
    [Header("Scriptable Object")]
    [Tooltip("Player statistics")] [SerializeField] 
    private PlayerStats stats;
    
    private Player player;
    
    /// Event triggered when the player's temperature reaches zero.
    public delegate void OnPlayerFreezed();
    public event OnPlayerFreezed playerFreezedEvent;

    private Coroutine temperatureCoroutine;
    
    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        // Only for Debug
        //if is only nosafe zone will loss a %

        if (Input.GetKeyDown(KeyCode.T))
        {
            LossTemperature(0.5f);
        }
    }
    
    /// <summary>
    /// Subtract temperature to the player's temperature and triggers frozen event if temperature falls to zero or below.
    /// </summary>
    /// <param buttonName="amount">The amount of temperature to loss.</param>
    public void LossTemperature(float amount)
    {
        stats.Temperature -= amount;
        if (stats.Temperature <= 0 && player.IsFrozen == false)
        {
            stats.Temperature = 0f;
            PlayerFrozen();
        }
    }

    public void RestoreTemperature(float amount)
    {
        stats.Temperature += amount;
        if (stats.Temperature > stats.MaxTemperature)
        {
            stats.Temperature = stats.MaxTemperature;
        }
    }

    public bool CanRestoreTemperature()
    {
        return stats.Temperature > 0 && stats.Temperature < stats.MaxTemperature;
    }
    
    /// <summary>
    /// Invokes the playerFrozenEvent to notify other components of the player's frozen.
    /// </summary>
    private void PlayerFrozen()
    {
        playerFreezedEvent?.Invoke();
    }

    /// <summary>
    /// Starts the coroutine to continuously lose temperature.
    /// </summary>
    public void StartLosingTemperature()
    {
        if (temperatureCoroutine != null) return;
        
        temperatureCoroutine = StartCoroutine(LoseTemperatureOverTime());
    }

    /// <summary>
    /// Stops the coroutine that continuously loses temperature.
    /// </summary>
    public void StopLosingTemperature()
    {
        if (temperatureCoroutine == null) return;
        
        StopCoroutine(temperatureCoroutine);
        temperatureCoroutine = null;
    }

    /// <summary>
    /// Coroutine to lose temperature over time.
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoseTemperatureOverTime()
    {
        while (true)
        {
            LossTemperature(amountToLoss * Time.deltaTime);
            yield return null;
        }
    }
    
}