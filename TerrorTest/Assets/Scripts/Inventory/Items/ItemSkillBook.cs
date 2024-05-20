using UnityEngine;

/// <summary>
/// The ItemSkillBook class represents a skill book item that can upgrade the player's skills.
/// This class inherits from InventoryItem and overrides the UseItem method to apply skill upgrades.
/// </summary>
[CreateAssetMenu(fileName = "ItemSkillBook", menuName = "Items/Skill Book")]
public class ItemSkillBook : InventoryItem
{
    [Header("Configuration")] 
    [Tooltip("Amount of points to upgrade")]
    public int amountPoints;

    [Tooltip("Type of skill to upgrade")]
    public SkillType SkillType;

    /// <summary>
    /// Uses the skill book to upgrade the specified skill type.
    /// </summary>
    /// <returns>True if the item was used successfully.</returns>
    public override bool UseItem()
    {
        UpgradeSkill(SkillType);
        return true;
    }

    /// <summary>
    /// Upgrades the specified skill type by the amount of points.
    /// </summary>
    /// <param name="skillType">The type of skill to upgrade.</param>
    private void UpgradeSkill(SkillType skillType)
    {
        switch (skillType)
        {
            case SkillType.Athletics:
                GameManager.Instance.Player.Stats.Athletics += amountPoints;
                break;
            case SkillType.Intimidation:
                GameManager.Instance.Player.Stats.Intimidation += amountPoints;
                break;
            case SkillType.Stealth:
                GameManager.Instance.Player.Stats.Stealth += amountPoints;
                break;
            case SkillType.Medicine:
                GameManager.Instance.Player.Stats.Medicine += amountPoints;
                break;
            case SkillType.Astrology:
                GameManager.Instance.Player.Stats.Astrology += amountPoints;
                break;
            case SkillType.Occultism:
                GameManager.Instance.Player.Stats.Occultism += amountPoints;
                break;
        }
    }
}

