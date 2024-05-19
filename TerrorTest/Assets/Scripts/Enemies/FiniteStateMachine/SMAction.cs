using UnityEngine;

/// <summary>
/// Abstract class for defining actions within a state machine.
/// </summary>
public abstract class SMAction : MonoBehaviour
{
    /// <summary>
    /// Defines an action to be performed by the state machine.
    /// </summary>
    public abstract void Act();
}
