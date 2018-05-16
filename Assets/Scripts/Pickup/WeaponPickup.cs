using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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

                NetworkServer.Spawn(CreatedWeapon);
                NetworkServer.Destroy(gameObject);

                CreatedWeapon.GetComponent<WeaponObject>().SetOwner(Object, weaponBase.WeaponParentNetID);
            }

            if (WeaponCount == 3 && Interacted) {
                int index = weaponBase.WeaponObject.transform.GetSiblingIndex();

                var NewPickup = Instantiate(
                    weaponBase.WeaponObject.GetComponent<WeaponObject>().PickupObject,
                    transform.position,
                    transform.rotation);

                NetworkServer.Spawn(NewPickup);
                NetworkServer.Destroy(weaponBase.WeaponObject);

                var CreatedWeapon = Instantiate(
                    Weapon,
                    weaponBase.WeaponParent.transform.position,
                    weaponBase.WeaponParent.transform.rotation);

                NetworkServer.Spawn(CreatedWeapon);
                NetworkServer.Destroy(gameObject);

                CreatedWeapon.GetComponent<WeaponObject>().SetOwner(Object, weaponBase.WeaponParentNetID);
                CreatedWeapon.transform.SetSiblingIndex(index);
                weaponBase.RpcSetWeapon(index);
            }
        }
    }
}
