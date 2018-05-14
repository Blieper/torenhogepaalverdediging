using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WeaponPickup : Pickup {

    public GameObject Weapon;

	public override void OnPickup (GameObject Object)
    {
        WeaponBase weaponBase = Object.GetComponent<WeaponBase>();

        if (weaponBase)
        {
            int WeaponCount = 0;

            foreach (Transform PWeapon in weaponBase.WeaponParent.transform)
            {
                WeaponCount++;

                if (PWeapon.gameObject.name.Replace("(Clone)","") == Weapon.name)
                {
                    return;
                }
            }

            var CreatedWeapon = Instantiate(
                Weapon,
                weaponBase.WeaponParent.transform.position,
                weaponBase.WeaponParent.transform.rotation);

            CreatedWeapon.GetComponent<WeaponObject>().SetOwner(Object);
            
            if (WeaponCount == 0)
            {
                weaponBase.SetWeapon(0);
            }

            GameObject.Destroy(gameObject);
        }
    }
}
