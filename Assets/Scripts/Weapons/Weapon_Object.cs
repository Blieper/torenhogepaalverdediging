using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Object : MonoBehaviour {

    // How many rounds per minute does the weapon fire?
    public float FireRate = 500;
    // How many bullets does the weapon fire per trigger-pull?
    // 0 = fully auto, 1 = semi auto, 2 = 2 round burst etc
    public int Burst = 0;
    public bool CompleteBurst = false;
    public int AmmoCapacity = 30;
    public int ReserveAmmo = 90;
    public float ReloadTime = 2f;
    public int AmmoAmount = 0;
    public float SwayWeight = 25f;
    public float Recoil = 2f;

    public Vector3 Offset = new Vector3(0, 0, 0);
    public GameObject ProjectileType;
    public GameObject Owner;
    public GameObject Muzzle;
    public weapon_base WeaponBase;

    void Start ()
    {
        AmmoAmount = AmmoCapacity;
    }

    public void SetOwner(GameObject NewOwner)
    {
        Owner = NewOwner;
        this.transform.SetParent(WeaponBase.WeaponObject.transform);
        this.transform.localPosition = Offset;
    }

    public void Activate ()
    {
        this.gameObject.SetActive(true);

        WeaponBase = Owner.GetComponent<weapon_base>();
        WeaponBase.FireRate = FireRate;
        WeaponBase.Burst = Burst;
        WeaponBase.CompleteBurst = CompleteBurst;
        WeaponBase.ReloadTime = ReloadTime;
        WeaponBase.Recoil = Recoil;
        WeaponBase.ProjectileType = ProjectileType;
        WeaponBase.FireDelay = 1f / (FireRate / 60);
        WeaponBase.Sway.Weight = SwayWeight;
        WeaponBase.Muzzle = Muzzle;
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
}
