using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Pickup {

    public GameObject Weapon;

	public override void OnPickup (GameObject Object, bool Interacted)
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

            if (WeaponCount < 3) {
                var CreatedWeapon = Instantiate(
                    Weapon,
                    weaponBase.WeaponParent.transform.position,
                    weaponBase.WeaponParent.transform.rotation);

                CreatedWeapon.GetComponent<WeaponObject>().SetOwner(Object);

                GameObject.Destroy(gameObject);
            } 
            
            if (WeaponCount == 0)
            {
                weaponBase.SetWeapon(0);
            }

            if (WeaponCount == 3 && Interacted) {
                int index = weaponBase.WeaponObject.transform.GetSiblingIndex();

                var NewPickup = Instantiate(
                    weaponBase.WeaponObject.GetComponent<WeaponObject>().PickupObject,
                    transform.position,
                    transform.rotation);
                
                GameObject.Destroy(weaponBase.WeaponObject);

                var CreatedWeapon = Instantiate(
                    Weapon,
                    weaponBase.WeaponParent.transform.position,
                    weaponBase.WeaponParent.transform.rotation);

                CreatedWeapon.GetComponent<WeaponObject>().SetOwner(Object);

                CreatedWeapon.transform.SetSiblingIndex(index);
                weaponBase.SetWeapon(index);

                GameObject.Destroy(gameObject);
            }
        }
    }
}
