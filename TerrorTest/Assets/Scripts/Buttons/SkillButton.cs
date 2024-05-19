using System;
using UnityEngine;
using UnityEngine.Serialization;

public class SkillButton : MonoBehaviour
{
    public static event Action<SkillType> OnSkillSelectedEvent;
    [Header("Configuration")]
    [Tooltip("SkillType")][SerializeField] 
    private SkillType skillType;
    public GameObject button;

    private void Awake()
    {
        button = this.gameObject;
    }

    public void SelectSkill()
    {
        OnSkillSelectedEvent?.Invoke(skillType);
    }
}
