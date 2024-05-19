using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [Header("Text Configuration")] 
    [Tooltip("Canvas damage TextMeshPro")] [SerializeField] 
    private TextMeshProUGUI damageTMP;

    public void SetDamageText(float damage)
    {
        damageTMP.text = damage.ToString();
    }

    public void DestroyText()
    {
        Destroy(gameObject);
    }
}
