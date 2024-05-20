using System;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// The SkillButton class handles the selection of a skill type through a button.
/// Triggers an event when a skill is selected.
/// </summary>
public class SkillButton : MonoBehaviour
{
    /// <summary>
    /// Event triggered when a skill is selected.
    /// </summary>
    public static event Action<SkillType> OnSkillSelectedEvent;

    [Header("Configuration")]
    [Tooltip("SkillType")]
    [SerializeField] 
    private SkillType skillType;

    /// <summary>
    /// The button GameObject associated with this skill.
    /// </summary>
    public GameObject button;

    private void Awake()
    {
        button = this.gameObject;
    }

    /// <summary>
    /// Invokes the OnSkillSelectedEvent with the selected skill type.
    /// </summary>
    public void SelectSkill()
    {
        OnSkillSelectedEvent?.Invoke(skillType);
    }
}