using UnityEngine;

/// <summary>
/// Manages the player's experience points and leveling up.
/// This script handles adding experience points, checking for level-ups, and applying level-up benefits.
/// </summary>
public class PlayerExp : MonoBehaviour
{
    [Header("Scriptable Objects to Add")]
    [Tooltip("Scriptable Object that contains player statistics")]
    [SerializeField] 
    private PlayerStats stats;

    private void Update()
    {
        // Only for Debug
        if (Input.GetKeyDown(KeyCode.X))
        {
            AddExp(300f);
        }
    }

    /// <summary>
    /// Adds experience points to the player and checks for level-ups.
    /// </summary>
    /// <param name="amount">The amount of experience points to add.</param>
    public void AddExp(float amount)
    {
        stats.TotalExperience += amount;
        stats.CurrentExp += amount;

        while (stats.CurrentExp >= stats.NextLevelExp)
        {
            stats.CurrentExp -= stats.NextLevelExp;
            NextLevel();
        }
    }

    /// <summary>
    /// Handles the actions to perform when the player levels up.
    /// </summary>
    private void NextLevel()
    {
        stats.Level++;
        stats.AttributePoints += 2;
        stats.SkillPoints += 2;

        float currentExpRequired = stats.NextLevelExp;
        float newNextLevelExp = Mathf.Round(currentExpRequired + currentExpRequired * (stats.ExpMultiplier / 100f));
        stats.NextLevelExp = newNextLevelExp;
    }
}