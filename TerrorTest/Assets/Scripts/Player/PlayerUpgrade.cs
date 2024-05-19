using System;
using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    public static event Action OnPlayerUpgradeEvent;

    [SerializeField] private PlayerStats stats;

    [Header("Settings")] [SerializeField] 
    private UpgradeSettings[] settings;

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
                stats.maxMedicine = stats.Knowledge;
                break;
        }

        stats.AttributePoints--;
        OnPlayerUpgradeEvent?.Invoke();
    }

    private void SkillCallback(SkillType skillType)
    {
        if (stats.SkillPoints == 0) return;

        switch (skillType)
        {
            case SkillType.Athletics:
                if (stats.Athletics >= stats.MaxAthletics)
                {
                    //Message maxAthletics you can have
                    return;
                }
                stats.Athletics++;
                break;
            case SkillType.Intimidation:
                if (stats.Intimidation >= stats.MaxIntimidation)
                {
                    //Message MaxIntimidation you can have
                    return;
                }
                stats.Intimidation++;
                break;
            case SkillType.Medicine:
                if (stats.Medicine >= stats.maxMedicine)
                {
                    //Message maxAthletics you can have
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
    
    private void OnEnable()
    {
        AttributeButton.OnAttributeSelectedEvent += AttributeCallback;
        SkillButton.OnSkillSelectedEvent += SkillCallback;
    }

    private void OnDisable()
    {
        AttributeButton.OnAttributeSelectedEvent -= AttributeCallback;
        SkillButton.OnSkillSelectedEvent -= SkillCallback;
    }
    
}

[Serializable]
    public class UpgradeSettings
    {
        public string Name;

        [Header("Values")] public float DamageUpgrade;
        public float CChanceUpgrade;
        public float CDamageUpgrade;
        public float HealthUpgrade;
        public float TemperatureUpgrade;
        public float MadnessUpgrade;
    }
