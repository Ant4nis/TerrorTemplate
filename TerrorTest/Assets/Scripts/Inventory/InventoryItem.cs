using UnityEngine;

public enum ItemType
{
    Weapon,     //weapons
    Scroll,     // Scrolls containing spells or hidden information.
    Document,   // Documents including maps, letters, and personal journals.
    Book,       // Ancient books that may be key to advancing the plot or update skills.
    Relic,      // Powerful artifacts with unique abilities, only for occultism masters. Others will suffer harsh penalties.
    Amulet,     // Amulets and talismans with magical or protective properties.
    Drugs,       // all kind of legal or illegal drugs such as medicines or unknown things.
    Light       // All kind of light objects.
}


[CreateAssetMenu(menuName = "Items/Item")]
public class InventoryItem : ScriptableObject
{
    [Header("Item Configuration")] 
    [Tooltip("Identifier")]
    public string ID;
    [Tooltip("Item name")]
    public string Name;
    [Tooltip("Item icon for ui")]
    public Sprite Icon;
    [Tooltip("Item description")]
    [TextArea] public string Description;

    [Header("Information")]
    [Tooltip("Item type to apply different options")]
    public ItemType ItemType;
    [Tooltip("If you can use it")]
    public bool IsConsumable;
    [Tooltip("If you can stack a defined quantity to a slot")]
    public bool IsStackable;
    [Tooltip("Define max quantity you can stack in a slot")]
    public int MaxStack;

    [Header("Magic Configuration")]
    //public Magic MagicProtection;
    //public Item ItemRequired;
    public float ProtectionRange;

    [Header("Required SkillType")]
    public bool NeedSkill;
    //public Player skill;
    public float SkillLevel;
    
    [HideInInspector]public int Quantity; // to get a variable of the quantity available 
    
    public InventoryItem CopyItem()
    {
        InventoryItem instance = Instantiate(this);
        return instance;
    }

    public virtual bool UseItem()
    {
        if (IsConsumable == false)
        {
            return false;
        }
        return true; //by default
    }

    public virtual void EquipItem()
    {
        
    }

    public virtual void DestroyItem()
    {
        
    }
}
