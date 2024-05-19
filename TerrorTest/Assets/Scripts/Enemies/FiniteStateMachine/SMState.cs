using System;
/// <summary>
/// Component of a state machine framework used to manage different states within a game, specifically for controlling AI behavior such as that of an enemy character
/// </summary>
[Serializable]
public class SMState
{
    public string ID; // Identifier for the state.
    public SMAction[] Actions; // Array of actions that occur in this state.
    public SMTransition[] Transitions; // Array of transitions to other states.

    /// <summary>
    /// Updates the state by executing actions and transitions.
    /// </summary>
    /// <param buttonName="enemyBrain">The enemy brain controlling the state machine.</param>
    public void UpdateState(EnemyBrain enemyBrain)
    {
        ExecuteActions();
        ExecuteTransitions(enemyBrain);
    }
    
    /// <summary>
    /// Executes all actions associated with this state.
    /// </summary>
    private void ExecuteActions()
    {
        for (int i = 0; i < Actions.Length; i++)
        {
            Actions[i].Act();
        }
    }

    /// <summary>
    /// Evaluates and executes state transitions based on decisions.
    /// </summary>
    /// <param buttonName="enemyBrain">The enemy brain controlling the state machine.</param>
    private void ExecuteTransitions(EnemyBrain enemyBrain)
    {
        if (Transitions == null || Transitions.Length <= 0) return;
        for (int i = 0; i < Transitions.Length; i++)
        {
            bool decisionResult = Transitions[i].Decision.Decide();
            if (decisionResult)
            {
                enemyBrain.ChangeState(Transitions[i].TrueState);
            }
            else
            {
                enemyBrain.ChangeState(Transitions[i].FalseState);
            }
        }
    }
}