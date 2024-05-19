using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(WarningAttribute))]
public class WarningDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        WarningAttribute warning = attribute as WarningAttribute;

        EditorGUI.HelpBox(position, warning.Message, MessageType.Warning);
        position.y += EditorGUIUtility.singleLineHeight * 1.5f;
        EditorGUI.PropertyField(position, property, label, true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) + EditorGUIUtility.singleLineHeight * 1.5f;
    }
}