using UnityEngine;

/// <summary>
/// Enumeration of different item types in the game.
/// </summary>
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


/// <summary>
/// Represents an item in the inventory.
/// </summary>
[CreateAssetMenu(menuName = "Items/Item")]
public class InventoryItem : ScriptableObject
{
    [Header("Item Configuration")] 
    [Tooltip("Identifier")]
    public string ID;

    [Tooltip("Item name")]
    public string Name;

    [Tooltip("Item icon for UI")]
    public Sprite Icon;

    [Tooltip("Item description")]
    [TextArea] 
    public string Description;

    [Header("Information")]
    [Tooltip("Item type to apply different options")]
    public ItemType ItemType;

    [Tooltip("If the item can be used")]
    public bool IsConsumable;

    [Tooltip("If the item can stack a defined quantity in a slot")]
    public bool IsStackable;

    [Tooltip("Defines the maximum quantity you can stack in a slot")]
    public int MaxStack;

   /* [Header("Magic Configuration")]
    [Tooltip("Protection range for magical items")]
    public float ProtectionRange;*/

    // Future implementations
   /* [Tooltip("Magic protection associated with the item")]
    public Magic MagicProtection;*/

    [Tooltip("Required item for certain functionalities")]
    public InventoryItem ItemRequired;

    [Header("Required SkillType")]
    [Tooltip("If the item requires a specific skill level to use")]
    public bool NeedSkill;

    // Future implementations
    [Tooltip("Player skill required to use the item")]
    public Player Skill;

    [Tooltip("The skill level required to use the item")]
    public float SkillLevel;

    /// <summary>
    /// The quantity of this item available in the inventory.
    /// </summary>
    [HideInInspector] 
    public int Quantity;

    /// <summary>
    /// Creates a copy of the inventory item.
    /// </summary>
    /// <returns>A new instance of the InventoryItem.</returns>
    public InventoryItem CopyItem()
    {
        InventoryItem instance = Instantiate(this);
        return instance;
    }

    /// <summary>
    /// Uses the item if it is consumable.
    /// </summary>
    /// <returns>True if the item was used, false otherwise.</returns>
    public virtual bool UseItem()
    {
        if (!IsConsumable)
        {
            return false;
        }
        return true; // by default
    }

    /// <summary>
    /// Equips the item.
    /// </summary>
    public virtual void EquipItem()
    {
        // Default implementation does nothing.
    }

    /// <summary>
    /// Destroys the item.
    /// </summary>
    public virtual void DestroyItem()
    {
        // Default implementation does nothing.
    }
}