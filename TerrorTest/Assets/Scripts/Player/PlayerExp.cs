using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    [Header("Scriptable Objects to Add")]
    [Tooltip("Scriptable Object that contains player statistics")] [SerializeField] 
    private PlayerStats stats;

    private void Update()
    {
        // Only for Debug

        if (Input.GetKeyDown(KeyCode.X))
        {
            AddExp(300f);
        }
    }
    
    public void AddExp(float amount)
    {
        stats.TotalExperience += amount;
        stats.CurrentExp += amount;

        while (stats.CurrentExp >= stats.NextLevelExp)
        {
            stats.CurrentExp -= stats.NextLevelExp;
            NextLevel();
        }
    }

    private void NextLevel()
    {
        stats.Level++;
        stats.AttributePoints += 2;
        stats.SkillPoints+= 2;
        float currentExpRequired = stats.NextLevelExp;
        float newNextLevelExp = Mathf.Round(currentExpRequired + stats.NextLevelExp * (stats.ExpMultiplier / 100f) );
        stats.NextLevelExp = newNextLevelExp;
    }
}
