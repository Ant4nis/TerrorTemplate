using TMPro;
using UnityEngine;

/// <summary>
/// Manages the damage text display in the game. This script updates the damage text and provides a method to destroy the text object.
/// </summary>
public class DamageText : MonoBehaviour
{
    [Header("Text Configuration")]
    [Tooltip("Canvas damage TextMeshPro")]
    [SerializeField]
    private TextMeshProUGUI damageTMP;

    /// <summary>
    /// Sets the damage text to display the specified damage amount.
    /// </summary>
    /// <param name="damage">The amount of damage to display.</param>
    public void SetDamageText(float damage)
    {
        damageTMP.text = damage.ToString();
    }

    /// <summary>
    /// Destroys the damage text object.
    /// </summary>
    public void DestroyText()
    {
        Destroy(gameObject);
    }
}