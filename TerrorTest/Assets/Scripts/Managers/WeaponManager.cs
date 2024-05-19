using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : Singleton<WeaponManager>
{
    [Header("Weapon Configuration")]
    [Tooltip("The image of the weapon icon.")] [SerializeField] 
    private Image weaponIcon;
    [Tooltip("The image of the weapon icon.")] [SerializeField] 
    private TextMeshProUGUI weaponAmmoTMP;

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
        //GameManager.Instance.Player.PlayerAttack.id = id;
        GameManager.Instance.Player.PlayerAttack.EquipWeapon(weapon);
    }
}
