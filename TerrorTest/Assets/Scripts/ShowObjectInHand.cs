using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class manages the weapons in hand, storing and deactivating them also at the start.
/// </summary>
public class ShowObjectInHand : MonoBehaviour
{
    /// <summary>
    /// List to store weapon GameObjects in hand.
    /// </summary>
    public List<GameObject> weaponInHand;

    /// <summary>
    /// Populates the weaponInHand list with all child GameObjects
    /// and then deactivates them.
    /// </summary>
    private void Start()
    {
        // Add all child GameObjects to the weaponInHand list
        foreach (Transform weapon in transform)
        {
            weaponInHand.Add(weapon.gameObject);
        }
        
        // Deactivate all weapons in the weaponInHand list
        for (int i = 0; i < weaponInHand.Count; i++)
        {
            weaponInHand[i].SetActive(false);
        }
    }

    /// <summary>
    /// Returns all weapons in hand.
    /// </summary>
    public List<GameObject> GetAllWeapons()    // DELETE [FOR TESTING]
    {
        return weaponInHand;
    }

    /// <summary>
    /// Activates the weapon in hand with the given ID.
    /// </summary>
    /// <param name="id">The ID of the weapon to activate.</param>
    public void ActivateWeapon(string id)
    {
        foreach (GameObject weapon in weaponInHand)
        {
            ObjectInHand objectInHand = weapon.GetComponent<ObjectInHand>();
            if (objectInHand != null && objectInHand.ID == id)
            {
                weapon.SetActive(true);
                Debug.Log($"Weapon {weapon.name} with ID {id} has been activated.");
                return;
            }
        }
        Debug.LogWarning($"Weapon with ID {id} not found.");
    }
}