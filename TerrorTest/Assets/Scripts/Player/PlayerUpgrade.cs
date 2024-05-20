using System;
using UnityEngine;

/// <summary>
/// Manages the player's attribute and skill upgrades. Listens for attribute and skill selection events
/// and applies the corresponding upgrades to the player's stats.
/// </summary>
public class PlayerUpgrade : MonoBehaviour
{
    /// <summary>
    /// Event triggered when the player is upgraded.
    /// </summary>
    public static event Action OnPlayerUpgradeEvent;

    [SerializeField] private PlayerStats stats;

    [Header("Settings")]
    [Tooltip("Array of upgrade settings for different attributes.")]
    [SerializeField]
    private UpgradeSettings[] settings;

    /// <summary>
    /// Applies the attribute upgrades to the player's stats based on the specified upgrade index.
    /// </summary>
    /// <param name="upgradeIndex">The index of the upgrade settings to apply.</param>
    private void UpgradePlayerAttribute(int upgradeIndex)
    {
        stats.BaseDamage += settings[upgradeIndex].DamageUpgrade;
        stats.TotalDamage += settings[upgradeIndex].DamageUpgrade;
        stats.MaxHealth += settings[upgradeIndex].HealthUpgrade;
        stats.MaxTemperature += settings[upgradeIndex].TemperatureUpgrade;
        stats.MaxMadness += settings[upgradeIndex].MadnessUpgrade;
        stats.CriticalChance += settings[upgradeIndex].CChanceUpgrade;
        stats.CriticalDamage += settings[upgradeIndex].CDamageUpgrade;
    }

    /// <summary>
    /// Callback method for attribute selection events. Upgrades the selected attribute if there are available attribute points.
    /// </summary>
    /// <param name="attributeType">The type of attribute selected.</param>
    private void AttributeCallback(AttributeType attributeType)
    {
        if (stats.AttributePoints == 0) return;

        switch (attributeType)
        {
            case AttributeType.Physics:
                UpgradePlayerAttribute(0);
                stats.PPhysics++;
                stats.MaxAthletics = stats.PPhysics;
                break;
            case AttributeType.Oratory:
                UpgradePlayerAttribute(1);
                stats.Oratory++;
                stats.MaxIntimidation = stats.Oratory;
                break;
            case AttributeType.Knowledge:
                UpgradePlayerAttribute(2);
                stats.Knowledge++;
                stats.MaxMedicine = stats.Knowledge;
                break;
        }

        stats.AttributePoints--;
        OnPlayerUpgradeEvent?.Invoke();
    }

    /// <summary>
    /// Callback method for skill selection events. Upgrades the selected skill if there are available skill points.
    /// </summary>
    /// <param name="skillType">The type of skill selected.</param>
    private void SkillCallback(SkillType skillType)
    {
        if (stats.SkillPoints == 0) return;

        switch (skillType)
        {
            case SkillType.Athletics:
                if (stats.Athletics >= stats.MaxAthletics)
                {
                    // Display a message indicating the max athletics level reached.
                    return;
                }
                stats.Athletics++;
                break;
            case SkillType.Intimidation:
                if (stats.Intimidation >= stats.MaxIntimidation)
                {
                    // Display a message indicating the max intimidation level reached.
                    return;
                }
                stats.Intimidation++;
                break;
            case SkillType.Medicine:
                if (stats.Medicine >= stats.MaxMedicine)
                {
                    // Display a message indicating the max medicine level reached.
                    return;
                }
                stats.Medicine++;
                break;
            case SkillType.Stealth:
                stats.Stealth++;
                break;
            case SkillType.Astrology:
                stats.Astrology++;
                break;
            case SkillType.Occultism:
                stats.Occultism++;
                break;
        }

        stats.SkillPoints--;
        OnPlayerUpgradeEvent?.Invoke();
    }

    /// <summary>
    /// Registers the attribute and skill selection event handlers.
    /// </summary>
    private void OnEnable()
    {
        AttributeButton.OnAttributeSelectedEvent += AttributeCallback;
        SkillButton.OnSkillSelectedEvent += SkillCallback;
    }

    /// <summary>
    /// Unregisters the attribute and skill selection event handlers.
    /// </summary>
    private void OnDisable()
    {
        AttributeButton.OnAttributeSelectedEvent -= AttributeCallback;
        SkillButton.OnSkillSelectedEvent -= SkillCallback;
    }
}

/// <summary>
/// Contains settings for upgrading player attributes.
/// </summary>
[Serializable]
public class UpgradeSettings
{
    public string Name;

    [Header("Values")]
    public float DamageUpgrade;
    public float CChanceUpgrade;
    public float CDamageUpgrade;
    public float HealthUpgrade;
    public float TemperatureUpgrade;
    public float MadnessUpgrade;
}
