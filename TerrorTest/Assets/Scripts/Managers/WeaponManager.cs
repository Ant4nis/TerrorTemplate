using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages weapon equipping and updates the UI with the weapon's icon and ammo.
/// </summary>
public class WeaponManager : Singleton<WeaponManager>
{
    [Header("Weapon Configuration")]
    [Tooltip("The image of the weapon icon.")]
    [SerializeField]
    private Image weaponIcon;

    [Tooltip("The TextMeshProUGUI component for displaying weapon ammo.")]
    [SerializeField]
    private TextMeshProUGUI weaponAmmoTMP;

    /// <summary>
    /// Equips the specified weapon and updates the UI.
    /// </summary>
    /// <param name="weapon">The weapon to equip.</param>
    /// <param name="id">The ID of the weapon to equip.</param>
    public void EquipWeapon(Weapon weapon, string id)
    {
        weaponIcon.sprite = weapon.Icon;
        weaponIcon.preserveAspect = true;
        weaponIcon.gameObject.SetActive(true);

        if (weapon.WeaponType == WeaponType.Distance)
        {
            weaponAmmoTMP.text = weapon.RequiredAmmo.ToString();
            weaponAmmoTMP.gameObject.SetActive(true);
        }
        else
        {
            weaponAmmoTMP.gameObject.SetActive(false);
        }

        GameManager.Instance.Player.PlayerAttack.EquipWeapon(weapon);
    }
}