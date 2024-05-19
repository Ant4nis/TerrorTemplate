
using System;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// An action that causes an entity to wander randomly within a specified range.
/// This class is a concrete implementation of SMAction for random movement.
/// </summary>
public class ActionWander : SMAction
{
    [Header("Movement Configuration")]
    [Tooltip("Enemy Speed")]
    [SerializeField] private float speed; // The speed at which the enemy moves.
    [Tooltip("The time between movement direction changes")]
    [SerializeField] private float wanderTime; // How often the enemy changes its wandering direction.
    [Tooltip("The range within which the enemy can move.")]
    [SerializeField] private Vector2 moveRange; // The range of random movement from the current position.

    private Vector3 movePosition; // The next position to move towards.
    private float timer; // Timer to track movement changes.
    private CircleCollider2D circleCollider;
    private EnemyBrain enemyBrain;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    /// <summary>
    /// Initializes the wandering action by setting the first random destination.
    /// </summary>
    private void Start()
    {
        GetNewDestination();
    }

    /// <summary>
    /// Performs the wandering action by moving towards a target position and recalculating as necessary.
    /// </summary>
    public override void Act()
    {
        timer -= Time.deltaTime;
        Vector3 moveDirection = (movePosition - transform.position).normalized;
        Vector3 movement = moveDirection * (speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePosition) >= 0.5f) // Maintain a minimum distance to avoid jittering.
        {
            transform.Translate(movement);
        }

        if (timer <= 0)
        {
            GetNewDestination();
            timer = wanderTime;
        }
    }

    /// <summary>
    /// Calculates a new random destination within the specified movement range.
    /// </summary>
    private void GetNewDestination()
    {
        float randomX = Random.Range(-moveRange.x, moveRange.x);
        float randomY = Random.Range(-moveRange.y, moveRange.y);

        movePosition = transform.position + new Vector3(randomX, randomY);
    }

   /* private bool CheckWall()
    {
        Vector3 moveDirection = (movePosition - transform.position).normalized;
        Vector2 start = new Vector2(transform.position.x, transform.position.y);
        float distance = circleCollider.radius + 0.05f;

        RaycastHit2D hit = Physics2D.Raycast(start, moveDirection.normalized, distance, player.wallMask);

        return hit.collider != null;
    }*/

    /// <summary>
    /// Draws gizmos in the Unity editor to visualize the movement range and the next target position.
    /// </summary>
    private void OnDrawGizmos()
    {
        if (moveRange != Vector2.zero)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(transform.position, moveRange * 2);
            Gizmos.DrawLine(transform.position, movePosition);
        }
    }
}
