using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_base : MonoBehaviour {

    // How many rounds per minute does the weapon fire?
    public float FireRate = 500;
    // How many bullets does the weapon fire per trigger-pull?
    // 0 = fully auto, 1 = semi auto, 2 = 2 round burst etc
    public int Burst = 0;
    public bool CompleteBurst = false;
    public float ReloadTime = 2f;
    public float Recoil = 2f;

    public GameObject ProjectileType;
    public GameObject WeaponParent;
    public GameObject WeaponObject;
    public GameObject CameraObject;

    public int SelectedWeaponID = 0;

    character_movement charmove;
    public Weapon_Sway Sway;

    float FireTime;
    public float FireDelay;
    int FiredBurst;
    bool IsFiring = false;

    void Start () {
        FireDelay = 1f / (FireRate / 60);
        charmove = transform.GetComponent<character_movement>();
        Sway = transform.GetComponent<Weapon_Sway>();

        SetWeapon(0);
    }

    void Reset ()
    {
        IsFiring = false;
        FiredBurst = 0;
    }

    void CheckBurst ()
    {
        FireTime = Time.time + FireDelay;
        FiredBurst++;

        if (FiredBurst == Burst && Burst > 0)
        {
            Reset();
        }
    }
    
    void Fire ()
    {
        if (!IsFiring)
        {
            return;
        }

        if (WeaponObject.GetComponent<Weapon_Object>().AmmoAmount > 0)
        {
            Sway.Impulse(new Vector2(Random.value * 2 - 1,Recoil));

            WeaponObject.GetComponent<Weapon_Object>().AmmoAmount--;

            GameObject projectile = Instantiate(ProjectileType, CameraObject.transform.position, CameraObject.transform.rotation);
            Physics.IgnoreCollision(projectile.transform.GetComponent<Collider>(), transform.GetComponent<CharacterController>(), true);
        }

        CheckBurst();
    }

    void Reload ()
    {
        if (WeaponObject.GetComponent<Weapon_Object>().AmmoAmount < WeaponObject.GetComponent<Weapon_Object>().AmmoCapacity 
            && WeaponObject.GetComponent<Weapon_Object>().ReserveAmmo > 0) {
            Invoke("AfterReload", ReloadTime);
        }
    }

    void AfterReload ()
    {
        Reset();
        int ammoChange = WeaponObject.GetComponent<Weapon_Object>().AmmoCapacity - WeaponObject.GetComponent<Weapon_Object>().AmmoAmount;
        WeaponObject.GetComponent<Weapon_Object>().ReserveAmmo -= ammoChange;
        WeaponObject.GetComponent<Weapon_Object>().AmmoAmount += ammoChange;
    }
	
	void Update () {

        if (Time.time > FireTime && IsFiring)
        {
            Fire();
        }

        if (Input.GetButtonDown("Fire1") && Time.time > FireTime)
        {
            Fire();
            IsFiring = true;
        }

        if (Input.GetButtonUp("Fire1") && !CompleteBurst)
        {
            Reset();
        }

        if (Input.GetButtonDown("Reload") && !IsFiring)
        {
            Reload();
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && !IsFiring)
        {
            SelectedWeaponID++;

            if (SelectedWeaponID >= WeaponParent.transform.childCount)
            {
                SelectedWeaponID = 0;
            }

            SetWeapon(SelectedWeaponID);
            print(SelectedWeaponID);
        } else if (Input.GetAxis("Mouse ScrollWheel") < 0 && !IsFiring)
        {
            SelectedWeaponID--;

            if (SelectedWeaponID <= 0)
            {
                SelectedWeaponID = WeaponParent.transform.childCount - 1;
            }

            SetWeapon(SelectedWeaponID);
            print(SelectedWeaponID);
        }
    }

    void SetWeapon (int ID)
    {
        int i = 0;
        foreach (Transform Weapon in WeaponParent.transform)
        {
            if (i == ID)
            {
                Weapon.GetComponent<Weapon_Object>().Activate();
                WeaponObject = Weapon.gameObject;
                Sway.WeaponObject = Weapon.gameObject;
            }
            else
            {
                Weapon.GetComponent<Weapon_Object>().Deactivate();
            }

            i++;
        }

        Reset();
        Sway.Deploy();
    }
}
