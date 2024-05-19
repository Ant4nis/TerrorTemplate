using System;
using UnityEngine;

/// <summary>
/// Implements a patrol action where an entity follows a set path of waypoints.
/// </summary>
public class ActionPatrol : SMAction
{
    [Warning("Have no function. Reverse path is not implemented yet.")]
    [Tooltip("If true, take the reverse path. If false, when reaching the last point returns to start point (0).")]
    [SerializeField] private bool reversePath; // Determines if the path should be reversed upon reaching the end.
    
    [Header("Movement Configuration")]
    [Tooltip("Enemy Speed")]
    [SerializeField] private float speed; // The movement speed of the entity.

    private Waypoint waypoint; // Reference to the Waypoint component which holds the patrol path points.
    private int pointIndex; // Current index in the waypoint array.
    private Vector3 nextPosition; // Next position in the patrol path to move towards.
    
    /// <summary>
    /// Initializes the patrol action by retrieving the Waypoint component.
    /// </summary>
    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    /// <summary>
    /// Calls the method to make the entity follow the patrol path.
    /// </summary>
    public override void Act()
    {
        FollowPath();
    }

    /// <summary>
    /// Moves the entity towards the current waypoint and updates the waypoint if reached.
    /// </summary>
    private void FollowPath()
    {
        transform.position = Vector3.MoveTowards(transform.position, GetCurrentPosition(), speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, GetCurrentPosition()) <= Mathf.Epsilon)
        {
            UpdateNextPosition();
        }
    }

    /// <summary>
    /// Updates the current waypoint index to the next one in the sequence, or loops back to the start.
    /// </summary>
    private void UpdateNextPosition()
    {
        // Increment the point index to move to the next waypoint.
        pointIndex++;
        // If we reach the end of the array, reset to the beginning.
        if (pointIndex > waypoint.Points.Length - 1)
        {
            pointIndex = 0;
        }
    }

    /// <summary>
    /// Retrieves the current waypoint's position from the Waypoint component.
    /// </summary>
    /// <returns>The position of the current waypoint.</returns>
    private Vector3 GetCurrentPosition()
    {
        return waypoint.GetPosition(pointIndex);
    }
}
