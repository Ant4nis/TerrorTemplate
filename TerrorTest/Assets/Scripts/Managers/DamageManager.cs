using System;
using UnityEngine;

/// <summary>
/// Manages the display of damage text in the game. 
/// Uses a damage text prefab to show the amount of damage dealt to an entity.
/// </summary>
public class DamageManager : Singleton<DamageManager>
{
    [Header("Script Configuration")]
    [Tooltip("Reference to the damage text prefab")] 
    [SerializeField] 
    private DamageText damageTextPrefab;

    /// <summary>
    /// Instantiates and displays the damage text at the specified location.
    /// </summary>
    /// <param name="damageAmount">The amount of damage to display.</param>
    /// <param name="parent">The transform of the parent object where the damage text will be displayed.</param>
    public void ShowDamageText(float damageAmount, Transform parent)
    {
        DamageText text = Instantiate(damageTextPrefab, parent);
        text.transform.position += Vector3.right * 0.5f; // Slight offset for visibility
        text.SetDamageText(damageAmount);
    }
}