using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Configuration")]
    [Tooltip("The speed of the projectile when it is fired.")] [SerializeField] 
    private float speed;
    [Tooltip("Maximum travel distance before the projectile is deactivated.")] [SerializeField] 
    private float maxDistance = 10f; // Distancia máxima que puede recorrer

    public Vector3 Direction { get; set; }
    public float Damage { get; set; }

    private Vector3 startPosition;
    private float distanceTravelled;

    private void OnEnable()
    {
        startPosition = transform.position; // Almacena la posición inicial cuando el proyectil es activado
        distanceTravelled = 0; // Restablecer la distancia recorrida
    }

    private void Update()
    {
        transform.Translate(Direction * (speed * Time.deltaTime));
        
        // Calcula la distancia recorrida
        distanceTravelled = Vector3.Distance(startPosition, transform.position);
        if (distanceTravelled > maxDistance)
        {
            gameObject.SetActive(false); // Desactiva el proyectil si excede la distancia máxima
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<IDamageable>()?.TakeDamage(Damage);
        gameObject.SetActive(false); // Desactivar también cuando impacta
    }
}