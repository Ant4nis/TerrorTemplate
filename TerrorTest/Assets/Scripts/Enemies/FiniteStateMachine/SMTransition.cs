using System;

/// <summary>
/// Represents a transition between states in a state machine, based on a decision.
/// </summary>
[Serializable]
public class SMTransition
{
    public SMDecision Decision; // Decision that triggers the transition.
    public string TrueState;    // State to transition to if the decision is true.
    public string FalseState;   // State to transition to if the decision is false.
}