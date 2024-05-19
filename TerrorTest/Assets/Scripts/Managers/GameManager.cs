using System;
using UnityEngine;

public class GameManager: Singleton<GameManager>
{
    
    [Header("Settings")]
    [Tooltip("Player")] [SerializeField] 
    private Player player;
    [SerializeField]
    private UIManager uiManager;

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

    public void AddPlayerExp(float expAmount)
    {
        PlayerExp playerExp = player.GetComponent<PlayerExp>();
        playerExp.AddExp(expAmount);
    }
}
