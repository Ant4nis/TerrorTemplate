using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Enumeration of different types of weapons.
/// </summary>
public enum WeaponType { Melee, Distance, Magic, Ammo }
/// <summary>
/// Enumeration of different types of ammo.
/// </summary>
public enum AmmoType { None, Arrow, BulletGun }

/// <summary>
/// Represents a weapon in the game, with various configurations for different weapon types and ammunition.
/// </summary>
[CreateAssetMenu(fileName = "Weapon_")]
public class Weapon : ScriptableObject
{
    [Header("Configuration")]
    [FormerlySerializedAs("initID")]
    [Tooltip("The ID to equip at the start.")]
    public string IdToEquipAtStart;

    [Tooltip("The icon representing the weapon.")]
    public Sprite Icon;

    [Tooltip("The type of the weapon (Melee, Distance, Magic, Ammo).")]
    public WeaponType WeaponType;

    [Tooltip("The damage dealt by the weapon.")]
    public float Damage;

    [Header("Projectile Configuration")]
    [Tooltip("The projectile prefab associated with the weapon.")]
    public Projectile ProjectilePrefab;

    [Tooltip("The type of ammo used by the weapon.")]
    public AmmoType AmmoType;

    [FormerlySerializedAs("ammunitionPerShot")]
    [Tooltip("The amount of ammo required per shot.")]
    public float RequiredAmmo;
}
