using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a safe area in the game where objects implementing IFreezable can recover temperature.
/// </summary>
public class SafeArea : MonoBehaviour
{
    [Header("Configuration")] 
    [Tooltip("The amount of temperature to recover while inside.")]
    [SerializeField]
    private float amount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<IFreezable>()?.StartRecoveringTemperature(amount);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.GetComponent<IFreezable>()?.StopRecoveringTemperature();
    }
}