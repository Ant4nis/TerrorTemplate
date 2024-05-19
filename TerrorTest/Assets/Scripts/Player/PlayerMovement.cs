using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Velocity")] 
    [Tooltip("The walking speed of the player in units per second.")] [SerializeField] 
    private float walkingSpeed;
    [Tooltip("The running speed of the player in units per second.")] [SerializeField] 
    private float runningSpeed;
    
    private float currentSpeed;

    [Header("Raycast Configuration")] 
    [Tooltip("The distance of the ray to check if is touching a wall")] [SerializeField] 
    private float rayDistance;

    public float WalkingSpeed => walkingSpeed;
    public float RunningSpeed => runningSpeed;
    public Vector2 MoveDirection => moveDirection;
    public Vector2 LastMoveDirection => lastMoveDirection;
    
    private PlayerActions actions;
    private PlayerAnimations playerAnimations;
    private Player player;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private CircleCollider2D circleCollider2D;
    
    private Vector2 lastMoveDirection;

    /// <summary>
    /// Initializes components at the start.
    /// </summary>
    private void Awake()
    {
        player = GetComponent<Player>();
        actions = new PlayerActions();
        rb = GetComponent<Rigidbody2D>();
        playerAnimations = GetComponent<PlayerAnimations>();       
        circleCollider2D = GetComponent<CircleCollider2D>();
        currentSpeed = walkingSpeed;
    }

    /// <summary>
    /// Reads input and updates speed every frame.
    /// </summary>
    private void Update()
    {
        if (player.IsAttacking) return;
        ReadMovement();
        UpdateSpeed();
    }

    /// <summary>
    /// Moves the player based on the input and current speed.
    /// </summary>
    private void FixedUpdate()
    {
        if (player.IsAttacking) return;
        Move();
    }

    /// <summary>
    /// Applies movement to the player's Rigidbody2D.
    /// </summary>
    private void Move()
    {
        if (player.IsDead || player.IsFrozen) return;
        rb.MovePosition(rb.position + moveDirection * (currentSpeed * Time.fixedDeltaTime));
    }

    /// <summary>
    /// Updates the player's speed based on whether they are running or walking.
    /// </summary>
    private void UpdateSpeed()
    {
        currentSpeed = actions.Movement.Run.ReadValue<float>() > 0.5f ? runningSpeed : walkingSpeed;
    }

    /// <summary>
    /// Reads the player's movement direction from input, checks for walls, and updates animations.
    /// </summary>
    private void ReadMovement()
    {
        
        Vector2 inputDirection = actions.Movement.Move.ReadValue<Vector2>().normalized;

        if (inputDirection != Vector2.zero)
        {
            moveDirection = inputDirection;
            lastMoveDirection = moveDirection; // Actualiza la última dirección de movimiento
        }
        else
        {
            moveDirection = Vector2.zero; // Detiene el movimiento pero mantiene la última dirección
        }

        bool isWallBlocking = CheckWall();

        if (moveDirection == Vector2.zero || (isWallBlocking && player.stopAtWall))
        {
            playerAnimations.SetMovingTypeTransition(false, currentSpeed);
        }
        else
        {
            playerAnimations.SetMovingTypeTransition(true, currentSpeed);
            playerAnimations.SetMoveAnimation(moveDirection);
        }
    }

    /// <summary>
    /// Checks for walls in the direction of movement using a raycast.
    /// </summary>
    /// <returns>True if a wall is detected; otherwise false.</returns>
    private bool CheckWall()
    {
        Vector2 start = new Vector2(transform.position.x, transform.position.y + circleCollider2D.offset.y);
        float distance = circleCollider2D.radius + rayDistance;

        RaycastHit2D hit = Physics2D.Raycast(start, moveDirection.normalized, distance, player.wallMask);

        return hit.collider != null;
    }

    /// <summary>
    /// Draws debug lines to visualize the collider's boundary and the raycast direction.
    /// </summary>
    private void OnDrawGizmos()
    {
        if (circleCollider2D != null)
        {
            float distance = circleCollider2D.radius + 0.05f;
        
            Vector2 direction = moveDirection.normalized;
            Vector2 colliderCenterPosition = (Vector2)transform.position + new Vector2(0, circleCollider2D.offset.y);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(colliderCenterPosition, colliderCenterPosition + direction * distance);
        }
    }

    /// <summary>
    /// Enables the player's actions when the component is enabled.
    /// </summary>
    private void OnEnable()
    {
        actions.Enable();
    }

    /// <summary>
    /// Disables the player's actions when the component is disabled.
    /// </summary>
    private void OnDisable()
    {
        actions.Disable();
    }
}
