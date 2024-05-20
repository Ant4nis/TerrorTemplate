using System;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// The AttributeButton class handles the selection of an attribute type through a button.
/// Triggers an event when an attribute is selected.
/// </summary>
public class AttributeButton : MonoBehaviour
{
    /// <summary>
    /// Event triggered when an attribute is selected.
    /// </summary>
    public static event Action<AttributeType> OnAttributeSelectedEvent;

    [Header("Configuration")]
    [Tooltip("AttributeType")]
    [SerializeField] 
    private AttributeType attributeType;

    /// <summary>
    /// The button GameObject associated with this attribute.
    /// </summary>
    public GameObject button;

    /// <summary>
    /// Invokes the OnAttributeSelectedEvent with the selected attribute type.
    /// </summary>
    public void SelectAttribute()
    {
        OnAttributeSelectedEvent?.Invoke(attributeType);
    }
}