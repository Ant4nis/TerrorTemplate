using UnityEngine;

/// <summary>
/// Manages the state machine for an enemy character.
/// </summary>
public class EnemyBrain : MonoBehaviour
{
    [Header("State")]
    [Tooltip("Initial State")] [SerializeField] 
    private string initState; // PatrolState.
    [Tooltip("Array of possible states the enemy can be in.")] [SerializeField] 
    private SMState[] states; 

    /// <summary>
    /// The current active state of the enemy.
    /// </summary>
    public SMState CurrentState { get; set; }

    /// <summary>
    /// Reference to the player object, if within a relevant range or sight.
    /// </summary>
    public Transform Player { get; set; }

    /// <summary>
    /// Sets the initial state of the enemy when the game starts.
    /// </summary>
    private void Start()
    {
        ChangeState(initState);
    }

    /// <summary>
    /// Updates the current state of the enemy every frame.
    /// </summary>
    private void Update()
    {
        CurrentState?.UpdateState(this);
    }

    /// <summary>
    /// Changes the state of the enemy to a new specified state.
    /// </summary>
    /// <param buttonName="newStateID">The ID of the new state to transition to.</param>
    public void ChangeState(string newStateID)
    {
        SMState newState = GetState(newStateID);
        if (newState == null) return;  // Ensures the new state exists before changing.
        
        CurrentState = newState;
    }

    /// <summary>
    /// Retrieves a state from the array of possible states based on its ID.
    /// </summary>
    /// <param buttonName="newStateID">The ID of the state to retrieve.</param>
    /// <returns>The matching state if found; otherwise, null.</returns>
    private SMState GetState(string newStateID)
    {
        for (int i = 0; i < states.Length; i++)
        {
            if (states[i].ID == newStateID)
            {
                return states[i];
            }
        }
        
        return null;  // Returns null if the state is not found in the array.
    }
}
