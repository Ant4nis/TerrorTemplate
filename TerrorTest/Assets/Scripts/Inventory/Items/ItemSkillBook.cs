using UnityEngine;

[CreateAssetMenu(fileName = "ItemSkillBook", menuName = "Items/Skill Book")]
public class ItemSkillBook : InventoryItem
{
    [Header("Configuration")] 
    [Tooltip("Amount of points to upgrade")]
    public int amountPoints;
    public SkillType SkillType;

    
    public override bool UseItem()
    {
        UpgradeSkill(SkillType);
        return true;
    }

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

