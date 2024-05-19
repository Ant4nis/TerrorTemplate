using System;
using UnityEngine;

/// <summary>
/// Manages the player ammunition.
/// ,
/// and updates it accordingly using the PlayerStats ScriptableObject.
/// </summary>
public class PlayerAmmo : MonoBehaviour
{
    [Header("Scriptable Objects to Add")]
    [Tooltip("Scriptable Object that contains player statistics")] [SerializeField] 
    private PlayerStats stats;

    public float currentAmmo { get; private set; }

    private void Start()
    {
        ReloadAmmo();
    }

    private void Update()
    {
        // Only for Debug
        if (Input.GetKeyDown(KeyCode.N))
        {
            UseAmmo(1f);
        }
    }
    
    
    /// <summary>
    /// Decreases the ammo amount by a specified amount, ensuring it does not exceed 0.
    /// </summary>
    /// <param buttonName="amount">The amount to decrease ammo by.</param>
    public void UseAmmo(float amount)
    {
        //Choose the lowest value between the two parameters
        stats.SpecialAmmo = Mathf.Min(stats.SpecialAmmo -= amount, stats.MaxAmmo);
        currentAmmo = stats.SpecialAmmo;
    }

    public void ReloadAmmo()
    {
        currentAmmo = stats.MaxAmmo;
    }
}