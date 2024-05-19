using UnityEngine;

/// <summary>
/// Defines an abstract base class for decisions in a state machine. 
/// Implement this class to create specific decision logic.
/// </summary>
public abstract class SMDecision : MonoBehaviour
{
    /// <summary>
    /// Decides the next action to take in a state machine.
    /// Must be implemented by derived classes to return true or false based on custom logic.
    /// </summary>
    /// <returns>Boolean result of the decision.</returns>
    public abstract bool Decide();
}