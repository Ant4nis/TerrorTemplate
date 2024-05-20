using UnityEngine;

/// <summary>
/// Represents a projectile in the game, which moves in a specified direction and deals damage on impact.
/// </summary>
public class Projectile : MonoBehaviour
{
    [Header("Projectile Configuration")]
    [Tooltip("The speed of the projectile when it is fired.")]
    [SerializeField] 
    private float speed;

    [Tooltip("Maximum travel distance before the projectile is deactivated.")]
    [SerializeField] 
    private float maxDistance = 10f;

    /// <summary>
    /// The direction in which the projectile travels.
    /// </summary>
    public Vector3 Direction { get; set; }

    /// <summary>
    /// The damage dealt by the projectile on impact.
    /// </summary>
    public float Damage { get; set; }

    private Vector3 startPosition;
    private float distanceTravelled;

    private void OnEnable()
    {
        startPosition = transform.position; // Store the initial position when the projectile is activated
        distanceTravelled = 0; // Reset the distance travelled
    }

    private void Update()
    {
        transform.Translate(Direction * (speed * Time.deltaTime));

        // Calculate the distance travelled
        distanceTravelled = Vector3.Distance(startPosition, transform.position);
        if (distanceTravelled > maxDistance)
        {
            gameObject.SetActive(false); // Deactivate the projectile if it exceeds the maximum distance
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<IDamageable>()?.TakeDamage(Damage);
        gameObject.SetActive(false); // Deactivate upon impact
    }
}