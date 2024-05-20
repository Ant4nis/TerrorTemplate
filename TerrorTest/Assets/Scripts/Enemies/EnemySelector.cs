using System;
using UnityEngine;

/// <summary>
/// Manages the selection visual indicator of an enemy entity.
/// Handles callbacks for when the enemy is selected or deselected.
/// </summary>
public class EnemySelector : MonoBehaviour
{
    [Header("Enemy Selection Configuration")]
    [Tooltip("Game Object that references the selector sprite.")]
    [SerializeField] 
    private GameObject selectorSprite;

    private EnemyBrain enemyBrain;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }

    /// <summary>
    /// Callback method to handle enemy selection.
    /// </summary>
    /// <param name="enemySelected">The selected enemy.</param>
    private void EnemySelectedCallback(EnemyBrain enemySelected)
    {
        selectorSprite.SetActive(enemySelected == enemyBrain);
    }

    /// <summary>
    /// Callback method to handle no selection.
    /// </summary>
    public void NoSelectionCallback()
    {
        selectorSprite.SetActive(false);
    }

    private void OnEnable()
    {
        SelectionManager.OnEnemySelectedEvent += EnemySelectedCallback;
        SelectionManager.OnNullSelectionEvent += NoSelectionCallback;
    }

    private void OnDisable()
    {
        SelectionManager.OnEnemySelectedEvent -= EnemySelectedCallback;
        SelectionManager.OnNullSelectionEvent -= NoSelectionCallback;
    }
}