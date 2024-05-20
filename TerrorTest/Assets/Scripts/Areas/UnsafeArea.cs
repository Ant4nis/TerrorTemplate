using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an unsafe area in the game where certain effects can occur.
/// This area can either cause objects implementing IFreezable to lose temperature or trigger horror events.
/// </summary>
public class UnsafeArea : MonoBehaviour
{
    [Header("Configuration")] 
    [Tooltip("If true, this area will cause objects with the IFreezable interface to lose temperature.")]
    [SerializeField]
    private bool coldArea;
    
    [Tooltip("The amount of temperature to lose while inside.")]
    [SerializeField]
    private float amount;

    [Tooltip("If true, this area will allow horror events to occur.")]
    [SerializeField]
    private bool horrorArea;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (coldArea)
        {
            other.GetComponent<IFreezable>()?.StartLosingTemperature(amount);
        }
        if (horrorArea)
        {
            // Activate enemy area to permitted
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (coldArea)
        {
            other.GetComponent<IFreezable>()?.StopLosingTemperature();
        }
        if (horrorArea)
        {
            // Deactivate enemy area to permitted
        }
    }
}