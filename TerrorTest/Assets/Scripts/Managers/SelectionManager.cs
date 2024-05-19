using System;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// About to select an enemy with the mouse Button
/// </summary>

public class SelectionManager : MonoBehaviour
{
    [Header("Selection Configuration")] [Tooltip("If you want to have enemy selection")] [SerializeField] 
    private bool enemySelection;
    [Tooltip("The layer mask of the enemy")] [SerializeField]
    private LayerMask enemyMask;

    private Camera mainCamera;

    public static event Action<EnemyBrain> OnEnemySelectedEvent; 
    public static event Action OnNullSelectionEvent;
    
    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (enemySelection == false) return;
        
        SelectEnemy();
    }

    private void SelectEnemy()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = 
                Physics2D.Raycast
                (mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,
                Mathf.Infinity, enemyMask);

            if (hit.collider != null)
            {
                EnemyBrain enemy = hit.collider.GetComponent<EnemyBrain>();
                if (enemy == null) return;
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth.CurrentHealth <= 0f) return;
                OnEnemySelectedEvent?.Invoke(enemy);   
            }
            else
            {
                OnNullSelectionEvent?.Invoke();
            }
        }
    }
}
