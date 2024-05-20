using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameContent))]
public class GameContentEditor : Editor
{
    
    /// Provides a property that returns the PlayerStats component attached to the target object.
    private GameContent ContentTarget => target as GameContent;

    /// <summary>
    /// Overrides the default inspector GUI to add custom editor functionality.
    /// This method adds a "Reset Stats" button to the inspector of the PlayerStats object.
    /// When pressed, this button calls the ResetStats method on the associated PlayerStats instance,
    /// allowing developers to quickly reset the player's stats during testing.
    /// </summary>
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); // Draw the default inspector.
        if (GUILayout.Button("Delete Inventory (Only use while game is started, if not null reference exception)")) // Add a button to the GUI with that buttonName.
        {
            ContentTarget.DeleteInventory(); // Reset stats when the button is pressed from PlayerStats.
        }
    }
}