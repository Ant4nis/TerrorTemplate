using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// A ScriptableObject that holds and manages a player's statistics like health, level, and madness. This allows for easy adjustment and scaling of player attributes within the Unity Editor.
/// </summary>

public enum AttributeType { Physics, Oratory, Knowledge }

public enum SkillType
{
    Athletics,
    Intimidation,
    Stealth,
    Medicine,
    Astrology,
    Occultism
}

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player Statistics")]
public class PlayerStats : ScriptableObject
{
    [Header("Statistics Configuration")]
    [Tooltip("The player's initial level.")]
    public int Level;

    [Header("Health")]
    [Tooltip("Initial health of the player.")]
    public float Health;
    [Tooltip("Maximum health the player can attain.")]
    public float MaxHealth;
    
    [Header("Temperature")]
    [Tooltip("Initial temperature of the player.")]
    public float Temperature;
    [Tooltip("Maximum temperature the player can withstand.")]
    public float MaxTemperature;
    
    [Header("Madness")]
    [Tooltip("Initial madness level of the player; higher levels can trigger special events.")]
    public float Madness;
    [Tooltip("Maximum level of madness the player can reach.")]
    public float MaxMadness;
    
    [Header("Experience")]
    [Tooltip("Current experience points of the player.")]
    public float CurrentExp;
    [Tooltip("Experience required to reach the next level.")]
    public float NextLevelExp;
    [Tooltip("Initial experience required for the first level up.")]
    public float InitialNextLevelExp;
    [Tooltip("Experience multiplier to decelerate leveling progression.")]
    [Range(1f, 100f)] public float ExpMultiplier;

    [Header("Attack")]
    [Tooltip("Base damage inflicted by the player.")]
    public float BaseDamage;
    [Tooltip("Additional critical damage applied to the base damage.")]
    public float CriticalDamage;
    [Tooltip("Percentage chance to inflict critical damage.")]
    public float CriticalChance;
    
    [FormerlySerializedAs("Ammo")]
    [Header("Special SpecialAmmo")]
    [Tooltip("Initial amount of special ammo the player starts with.")]
    public float SpecialAmmo;
    [Tooltip("Maximum amount of special ammo the player can hold.")]
    public float MaxAmmo;

    [Header("Attributes")]
    [Tooltip("Player's level in physical abilities.")]
    public int PPhysics;
    [Tooltip("Player's level in oratory skills.")]
    public int Oratory;
    [Tooltip("Player's level in knowledge-based activities.")]
    public int Knowledge;
    [Tooltip("Player's points to increase attributes.")]
    public int AttributePoints;
    
    [Header("Skills")]
    [Tooltip("Player's points to increase skills.")]
    public int SkillPoints;
    [Tooltip("Player's proficiency in athletics.")]
    public int Athletics;
    [Tooltip("Player's proficiency in intimidation.")]
    public int Intimidation;
    [Tooltip("Player's proficiency in stealth.")]
    public int Stealth;
    [Tooltip("Player's proficiency in medicine.")]
    public int Medicine;
    [Tooltip("Player's proficiency in astrology.")]
    public int Astrology;
    [Tooltip("Player's proficiency in occultism.")]
    public int Occultism;

    [FormerlySerializedAs("MaxAthletism")]
    [Header("Skill Restrictions")]
    [Tooltip("The max athletics is equal your strength stat")]
    public int MaxAthletics;
    [Tooltip("The max athletics is equal your oratory stat")]
    public int MaxIntimidation;
    [Tooltip("The max athletics is equal your knowledge stat")]
    public int maxMedicine;
    
    [HideInInspector]
    public float TotalExperience;
    [HideInInspector]
    public float TotalDamage;
    
    /// <summary>
    /// Resets the player's statistics to their maximum values across all configurable fields. This method is typically used to restore the player's condition to full capacity at the start of a game or after a revival.
    /// </summary>
    public void ResetStats()
    {
        Level = 1;
        
        Madness = 0f;
        MaxHealth = 10;
        MaxTemperature = 50;
        MaxMadness = 50;
        Health = MaxHealth;
        Temperature = MaxTemperature;
        
        CurrentExp = 0f;
        TotalExperience = 0f;
        NextLevelExp = InitialNextLevelExp;

        BaseDamage = 1f;
        CriticalChance = 10f;
        CriticalDamage = 10f;
        
        PPhysics = 0;
        Oratory = 0;
        Knowledge = 0;
        
        AttributePoints = 4;
        Athletics = 0;
        Intimidation = 0;
        Stealth = 0;
        Medicine = 0;
        Astrology = 0;
        Occultism = 0;
        SkillPoints = 4;
        
        
        SpecialAmmo = MaxAmmo;
        MaxAthletics = PPhysics;
        MaxIntimidation = Oratory;
        maxMedicine = Knowledge;
    }

    /// <summary>
    /// Resets only the essential health-related statistics of the player to their maximum values. Useful for scenarios where only partial recovery is needed.
    /// </summary>
    public void MinimumResetStats()
    {
        Health = MaxHealth;
        Temperature = MaxTemperature;
        Madness = 0f;
    }
}
