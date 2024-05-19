using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   [Header("Scriptable Object")] [Tooltip("Player Stats.")] [SerializeField]
   private PlayerStats stats;

   public GameObject[] attributeButtons;
   public GameObject[] skillButtons;
   
   [Header("Bars")] [Tooltip("UI Health Bar.")] [SerializeField]
   private Image healthBar;

   [Tooltip("UI Temperature Bar.")] [SerializeField]
   private Image temperatureBar;

   [Tooltip("UI Madness Bar.")] [SerializeField]
   private Image madnessBar;

   [Tooltip("UI Experience Bar.")] [SerializeField]
   private Image expBar;

   [Tooltip("UI Experience Bar.")] [SerializeField]
   private Image ammoBar;

   [Header("Texts")]
   /*[Tooltip("UI Health amount")]
   [SerializeField] private TextMeshProUGUI healthTMP;
   [Tooltip("UI Temperature amount")]
   [SerializeField] private TextMeshProUGUI temperatureTMP;
   [Tooltip("UI Madness amount")]
   [SerializeField] private TextMeshProUGUI madnessTMP;*/
   [Tooltip("UI ammo amount")] [SerializeField]
   private TextMeshProUGUI ammoTMP;
   [Tooltip("UI level amount")] [SerializeField]
   private TextMeshProUGUI LevelTMP;

   [Header("Panel")] 
   [Tooltip("The game object that contains base panel.")] 
   public GameObject basePanel;

   [Header("Status Panel")] 
   [Tooltip("Game object with status panel")] [SerializeField]
   private GameObject statusPanel;
   [Tooltip("The text with the level")] [SerializeField]
   private TextMeshProUGUI statLevelTMP;
   
   [Header("Stats Panel")] 
   [Tooltip("The game object that contains stats panel.")] [SerializeField]
   private GameObject statsPanel;
   
   /*[Tooltip("Text with the damage.")] [SerializeField]
   private TextMeshProUGUI statDamageTMP;
   [Tooltip("Text with the critical chance.")] [SerializeField]
   private TextMeshProUGUI statCChanceTMP;
   [Tooltip("Text with the critical damage.")] [SerializeField]
   private TextMeshProUGUI statCDamageTMP;
   [Tooltip("Text with the total XP.")] [SerializeField]
   private TextMeshProUGUI statTotalXPTMP;
   [Tooltip("Text with the current XP.")] [SerializeField]
   private TextMeshProUGUI statCurrentXPTMP;
   [Tooltip("Text with the XP needed.")] [SerializeField]
   private TextMeshProUGUI statNeedXPTMP;*/
   [Tooltip("Text with the points to upgrade attributes.")] [SerializeField]
   private TextMeshProUGUI attributePointsTMP;
   [Tooltip("Text with the physics level.")] [SerializeField]
   private TextMeshProUGUI physicsTMP;
   [Tooltip("Text with the oratory level.")] [SerializeField]
   private TextMeshProUGUI oratoryTMP;
   [Tooltip("Text with the knowledge level.")] [SerializeField]
   private TextMeshProUGUI knowledgeTMP;
   [Tooltip("Text with the points to upgrade skills.")] [SerializeField]
   private TextMeshProUGUI skillPointsTMP;
   [Tooltip("Text with the athletism level.")] [SerializeField]
   private TextMeshProUGUI athletismTMP;
   [Tooltip("Text with the intimidation level.")] [SerializeField]
   private TextMeshProUGUI intimidationTMP;
   [Tooltip("Text with the medicine level")] [SerializeField]
   private TextMeshProUGUI medicineTMP;
   [Tooltip("Text with the stealth level")] [SerializeField]
   private TextMeshProUGUI stealthTMP;
   [Tooltip("Text with the astrology level")] [SerializeField]
   private TextMeshProUGUI astrologyTMP;
   [Tooltip("Text with the occultism level")] [SerializeField]
   private TextMeshProUGUI occultismTMP;

   

   private void Start()
   {
      for (int i = 0; i < attributeButtons.Length; i++)
      {
         attributeButtons[i].gameObject.SetActive(false);
      }

      for (int i = 0; i < skillButtons.Length; i++)
      {
         skillButtons[i].gameObject.SetActive(false);
      }
   }

   private void Update()
   {
      UpdatePlayerUI();
      UpdateStatsPanel();
   }

   public void OpenCloseStatsPanel()
   {
      if (statusPanel.activeSelf)
      {
         statusPanel.SetActive(false);
         basePanel.SetActive(false);
      }
      else if (InventoryUI.Instance.inventoryPanel.activeSelf)
      {
         InventoryUI.Instance.inventoryPanel.SetActive(false);
         basePanel.SetActive(false);
      }
      basePanel.SetActive(!basePanel.activeSelf);
      statsPanel.SetActive(!statsPanel.activeSelf);
      
     /* if (statsPanel.activeSelf)
      {
         UpdateStatsPanel();
      }*/
   }
   
   public void OpenCloseStatusPanel()
   {
      if (statsPanel.activeSelf)
      {
         statsPanel.SetActive(false);
         basePanel.SetActive(false);
      }
      else if (InventoryUI.Instance.inventoryPanel.activeSelf)
      {
         InventoryUI.Instance.inventoryPanel.SetActive(false);
         basePanel.SetActive(false);
      }
      basePanel.SetActive(!basePanel.activeSelf);
      statusPanel.SetActive(!statusPanel.activeSelf);
   }
   
   public void CloseAllPanels()
   {
      basePanel.SetActive(false);
      statsPanel.SetActive(false);
      statusPanel.SetActive(false);
      InventoryUI.Instance.inventoryPanel.SetActive(false);
   }

   public void OpenStatsPanel()
   {
      basePanel.SetActive(true);
      statsPanel.SetActive(true);
   }
   public void OpenStatusPanel()
   {
      basePanel.SetActive(true);
      statusPanel.SetActive(true);
   }

   private void UpdatePlayerUI()
   {
      healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, stats.Health / stats.MaxHealth, 10f * Time.deltaTime);
      temperatureBar.fillAmount = Mathf.Lerp(temperatureBar.fillAmount, stats.Temperature / stats.MaxTemperature, 10f * Time.deltaTime);
      madnessBar.fillAmount = Mathf.Lerp(madnessBar.fillAmount, stats.Madness / stats.MaxMadness, 10f * Time.deltaTime);
      expBar.fillAmount = Mathf.Lerp(expBar.fillAmount, stats.CurrentExp / stats.NextLevelExp, 10f * Time.deltaTime);
      //ammoBar.fillAmount = Mathf.Lerp(ammoBar.fillAmount, stats.SpecialAmmo / stats.MaxAmmo, 10f * Time.deltaTime);
      
      LevelTMP.text = $"{stats.Level}";
      //ammoTMP.text = $"{stats.SpecialAmmo}";
   }

   private void UpdateStatsPanel()
   {
      statLevelTMP.text = stats.Level.ToString();
      
      
      /* statDamageTMP.text = stats.TotalDamage.ToString();
      statCChanceTMP.text = stats.CriticalChance.ToString();
      statCDamageTMP.text = stats.CriticalDamage.ToString();
      statTotalXPTMP.text = stats.TotalExperience.ToString();
      statCurrentXPTMP.text = stats.CurrentExp.ToString();
      statNeedXPTMP.text = stats.NextLevelExp.ToString();*/

      attributePointsTMP.text = stats.AttributePoints.ToString();
      
      if (stats.AttributePoints > 0)
      {
         for (int i = 0; i < attributeButtons.Length; i++)
         {
            attributeButtons[i].gameObject.SetActive(true);
         }

         attributePointsTMP.color = Color.green; // if points > 0
      }
      else
      {
         attributePointsTMP.color = Color.grey; // if 0 points
         for (int i = 0; i < attributeButtons.Length; i++)
         {
            attributeButtons[i].gameObject.SetActive(false);
         }
      }

      physicsTMP.text = stats.PPhysics.ToString();
      oratoryTMP.text = stats.Oratory.ToString();
      knowledgeTMP.text = stats.Knowledge.ToString();

      skillPointsTMP.text = stats.SkillPoints.ToString();
      if (stats.SkillPoints > 0)
      {
         for (int i = 0; i < skillButtons.Length; i++)
         {
            skillButtons[i].gameObject.SetActive(true);
         }

         skillPointsTMP.color = Color.green;
      }
      else
      {
         for (int i = 0; i < skillButtons.Length; i++)
         {
            skillButtons[i].gameObject.SetActive(false);
         }
         skillPointsTMP.color = Color.grey;
      }

      athletismTMP.text = stats.Athletics.ToString();
      intimidationTMP.text = stats.Intimidation.ToString();
      medicineTMP.text = stats.Medicine.ToString();
      stealthTMP.text = stats.Stealth.ToString();
      astrologyTMP.text = stats.Astrology.ToString();
      occultismTMP.text = stats.Occultism.ToString();

   }
   
   private void UpgradeCallback()
   {
      UpdateStatsPanel();
   }
   private void OnEnable()
   {
      PlayerUpgrade.OnPlayerUpgradeEvent += UpgradeCallback;
   }

   private void OnDisable()
   {
      PlayerUpgrade.OnPlayerUpgradeEvent -= UpgradeCallback;
   }
}
