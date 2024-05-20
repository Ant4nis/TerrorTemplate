using UnityEngine;
using UnityEditor;

/// <summary>
/// Custom property drawer for the WarningAttribute. Displays a warning message above the property field in the inspector.
/// </summary>
[CustomPropertyDrawer(typeof(WarningAttribute))]
public class WarningDrawer : PropertyDrawer
{
    /// <summary>
    /// Draws the property with a warning message in the inspector.
    /// </summary>
    /// <param name="position">The rectangle on the screen to use for the property GUI.</param>
    /// <param name="property">The SerializedProperty to make the custom GUI for.</param>
    /// <param name="label">The label of the property.</param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        WarningAttribute warning = attribute as WarningAttribute;

        EditorGUI.HelpBox(position, warning.Message, MessageType.Warning);
        position.y += EditorGUIUtility.singleLineHeight * 1.5f;
        EditorGUI.PropertyField(position, property, label, true);
    }

    /// <summary>
    /// Gets the height of the property including the warning message.
    /// </summary>
    /// <param name="property">The SerializedProperty whose height is being calculated.</param>
    /// <param name="label">The label of the property.</param>
    /// <returns>The height in pixels of the property including the warning message.</returns>
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) + EditorGUIUtility.singleLineHeight * 1.5f;
    }
}