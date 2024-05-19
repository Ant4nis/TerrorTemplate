using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a set of waypoints for navigation purposes. Waypoints are relative to the starting position of the GameObject.
/// </summary>
public class Waypoint : MonoBehaviour
{
    [Header("Move points Configuration")]
    [Tooltip("The number and positions of the waypoint")] [SerializeField] 
    private Vector3[] points; // Array of points representing waypoints.

    /// <summary>
    /// Provides read-only access to the waypoint positions.
    /// </summary>
    public Vector3[] Points => points;

    /// <summary>
    /// Stores the initial position of the GameObject to calculate waypoint positions relative to it.
    /// </summary>
    public Vector3 EntityPosition { get; set; }

    private bool gameStarted; // Flag to check if the game has started.

    /// <summary>
    /// Initializes the waypoint system by setting the entity's initial position.
    /// </summary>
    private void Start()
    {
        EntityPosition = transform.position;
        gameStarted = true;
    }

    /// <summary>
    /// Returns the world position of a specific waypoint index.
    /// </summary>
    /// <param buttonName="pointIndex">Index of the waypoint to retrieve.</param>
    /// <returns>World position of the waypoint.</returns>
    public Vector3 GetPosition(int pointIndex)
    {
        return EntityPosition + points[pointIndex]; // Returns the absolute position based on the initial position.
    }

    /// <summary>
    /// Draws gizmos in the editor and updates the entity's position if the transform has changed before the game starts.
    /// </summary>
    private void OnDrawGizmos()
    {
        if (!gameStarted && transform.hasChanged)
        {
            EntityPosition = transform.position; // Updates the initial position if the GameObject is moved in the editor.
        }
    }
}