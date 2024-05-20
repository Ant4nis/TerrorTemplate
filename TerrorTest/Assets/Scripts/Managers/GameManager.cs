using System;
using UnityEngine;

/// <summary>
/// Manages the overall game state, including player interactions and UI panel toggling.
/// </summary>
public class GameManager : Singleton<GameManager>
{
    [Header("Settings")]
    [Tooltip("Reference to the player object.")]
    [SerializeField] 
    private Player player;

    [Tooltip("Reference to the UI Manager.")]
    [SerializeField]
    private UIManager uiManager;

    /// <summary>
    /// Gets the reference to the player object.
    /// </summary>
    public Player Player => player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            player.RevivePlayer();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            uiManager.OpenCloseStatsPanel();
        }
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            uiManager.OpenCloseStatusPanel();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryUI.Instance.OpenCloseInventoryPanel();
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            uiManager.CloseAllPanels();
        }
    }

    /// <summary>
    /// Adds experience points to the player.
    /// </summary>
    /// <param name="expAmount">The amount of experience to add.</param>
    public void AddPlayerExp(float expAmount)
    {
        PlayerExp playerExp = player.GetComponent<PlayerExp>();
        playerExp.AddExp(expAmount);
    }
}