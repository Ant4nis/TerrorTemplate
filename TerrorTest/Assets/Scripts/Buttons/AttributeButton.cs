using System;
using UnityEngine;
using UnityEngine.Serialization;

public class AttributeButton : MonoBehaviour
{
    public static event Action<AttributeType> OnAttributeSelectedEvent;
    [Header("Configuration")]
    [Tooltip("AttributeType")][SerializeField] 
    private AttributeType attributeType;
    public GameObject button;

    public void SelectAttribute()
    {
        OnAttributeSelectedEvent?.Invoke(attributeType);
    }
}
