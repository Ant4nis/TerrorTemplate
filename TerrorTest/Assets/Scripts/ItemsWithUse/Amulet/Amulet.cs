using UnityEngine;


[CreateAssetMenu(fileName = "Amulet_")]
public class Amulet : ScriptableObject
{
    [Header("Image")]
    public Sprite Icon;
    
    [Header("Skill Configuration")]
    [Tooltip("If you need a skill to use")]
    public bool NeedSkill;
    [Tooltip("What skill we need")]
    public SkillType skillNeeded;
    [Tooltip("Level needed to use")]
    public int SkillLevel;

    [Header("Magic Configuration")] 
    [Tooltip("If you want to use a madness stat as ammo")]
    public bool needSpecialAmmo;
    [Tooltip("any enemy can cross the range")]
    public float ProtectionRange;
    
}
