using UnityEngine;

/// <summary>
/// Manages the madness level of the player character.
/// This component listens for specific triggers to increment the madness stat,
/// and updates it accordingly using the PlayerStats ScriptableObject.
/// </summary>
public class PlayerMadness : MonoBehaviour
{
    [Header("Scriptable Objects to Add")]
    [Tooltip("Scriptable Object that contains player statistics")] [SerializeField] 
    private PlayerStats stats;

    private void Update()
    {
        // Only for Debug
        if (Input.GetKeyDown(KeyCode.M))
        {
            AddMadness(1f);
        }
    }
    
    /// <summary>
    /// Increases the madness level by a specified amount, ensuring it does not exceed the maximum madness.
    /// </summary>
    /// <param buttonName="amount">The amount to increase the madness by.</param>
    public void AddMadness(float amount)
    {
        if (stats.Madness <= stats.MaxMadness)
        {
            //Choose the lowest value between the two parameters
            stats.Madness = Mathf.Min(stats.Madness += amount, stats.MaxMadness);
            
        }
    }

    public void RestoreMadness(float amount)
    {
        stats.Madness -= amount;
        if (stats.Madness < 0)
        {
            stats.Madness = 0;
        }
    }
    
    public bool CanRestoreMadness()
    {
        return stats.Madness > 0;
    }
}
