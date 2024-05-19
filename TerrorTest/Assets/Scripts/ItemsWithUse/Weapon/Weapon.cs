using UnityEngine;
using UnityEngine.Serialization;

public enum WeaponType { Melee, Distance, Magic, Ammo }
public enum AmmoType { None, Arrow, BulletGun }

[CreateAssetMenu(fileName = "Weapon_")]
public class Weapon : ScriptableObject
{ 
    [FormerlySerializedAs("initID")] [Header("Configuration")] 
    public string IdToEquipAtStart;
    public Sprite Icon;
    public WeaponType WeaponType;
    public float Damage;

    [Header("Projectile Configuration")]
    public Projectile ProjectilePrefab;
    public AmmoType AmmoType;
    [FormerlySerializedAs("ammunitionPerShot")] public float RequiredAmmo;

    
    
}
