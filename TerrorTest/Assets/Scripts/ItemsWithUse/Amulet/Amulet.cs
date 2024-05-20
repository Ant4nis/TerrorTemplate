using UnityEngine;

/// <summary>
/// Enumeration of the types of protection an amulet can provide.
/// </summary>
public enum TypeOfProtection
{
    /// <summary>
    /// Protection against cold.
    /// </summary>
    cold,

    /// <summary>
    /// Protection against horror.
    /// </summary>
    horror
}

/// <summary>
/// Represents an amulet in the game, which can provide various types of protection and may require specific skills to use.
/// </summary>
[CreateAssetMenu(fileName = "Amulet_")]
public class Amulet : ScriptableObject
{
    [Header("Image")]
    [Tooltip("The icon representing the amulet.")]
    public Sprite Icon;
    
    [Header("Skill Configuration")]
    [Tooltip("If a skill is needed to use the amulet.")]
    public bool NeedSkill;
    [Tooltip("The skill type required to use the amulet.")]
    public SkillType skillNeeded;
    [Tooltip("The skill level required to use the amulet.")]
    public int SkillLevel;

    [Header("Magic Configuration")] 
    [Tooltip("If the amulet uses a madness stat as ammo.")]
    public bool UseMadness;
    [Tooltip("The type of protection the amulet provides.")]
    public TypeOfProtection protection;
    [Tooltip("The range within which the amulet provides protection.")]
    public float ProtectionRange;
}