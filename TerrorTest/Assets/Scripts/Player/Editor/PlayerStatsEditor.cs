using UnityEditor;
using UnityEngine;
/// <summary>
/// Custom editor for the PlayerStats component in the Unity Editor.
/// This editor extends the default inspector functionality to include a "Reset Stats" button,
/// allowing when developing to easily reset player statistics directly from the editor.
/// </summary>
[CustomEditor(typeof(PlayerStats))]
public class PlayerStatsEditor : Editor
{
    
    /// Provides a property that returns the PlayerStats component attached to the target object.
    private PlayerStats StatsTarget => target as PlayerStats;

    /// <summary>
    /// Overrides the default inspector GUI to add custom editor functionality.
    /// This method adds a "Reset Stats" button to the inspector of the PlayerStats object.
    /// When pressed, this button calls the ResetStats method on the associated PlayerStats instance,
    /// allowing developers to quickly reset the player's stats during testing.
    /// </summary>
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); // Draw the default inspector.
        if (GUILayout.Button("Reset Stats")) // Add a button to the GUI with that buttonName.
        {
            StatsTarget.ResetStats(); // Reset stats when the button is pressed from PlayerStats.
        }
    }
}
