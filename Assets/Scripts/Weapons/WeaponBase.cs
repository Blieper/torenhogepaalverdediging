using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour {

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
    public GameObject Muzzle;

    public int SelectedWeaponID = 0;

    CharacterMovement charmove;
    public WeaponSway Sway;

    float FireTime;
    public float FireDelay;
    int FiredBurst;
    bool IsFiring = false;

    void Start() {
        FireDelay = 1f / (FireRate / 60);
        charmove = transform.GetComponent<CharacterMovement>();
        Sway = transform.GetComponent<WeaponSway>();

        SetWeapon(0);
    }

    void Reset() {
        IsFiring = false;
        FiredBurst = 0;
    }

    void CheckBurst() {
        FireTime = Time.time + FireDelay;
        FiredBurst++;

        if (FiredBurst == Burst && Burst > 0) {
            Reset();
        }
    }

    void Fire() {
        if (!IsFiring) {
            return;
        }

        if (WeaponObject.GetComponent<WeaponObject>().AmmoAmount > 0) {
            Sway.Impulse(new Vector2(Random.value * 2 - 1, Recoil));

            WeaponObject.GetComponent<WeaponObject>().AmmoAmount--;

            GameObject projectile = Instantiate(ProjectileType, CameraObject.transform.position, CameraObject.transform.rotation);
            projectile.GetComponent<Projectile>().Initialise(gameObject, Muzzle.transform.position, CameraObject.transform.position);
        }

        CheckBurst();
    }

    void Reload() {
        if (WeaponObject.GetComponent<WeaponObject>().AmmoAmount < WeaponObject.GetComponent<WeaponObject>().AmmoCapacity
            && WeaponObject.GetComponent<WeaponObject>().ReserveAmmo > 0) {
            Invoke("AfterReload", ReloadTime);
        }
    }

    void AfterReload() {
        Reset();
        int ammoChange = WeaponObject.GetComponent<WeaponObject>().AmmoCapacity - WeaponObject.GetComponent<WeaponObject>().AmmoAmount;
        WeaponObject.GetComponent<WeaponObject>().ReserveAmmo -= ammoChange;
        WeaponObject.GetComponent<WeaponObject>().AmmoAmount += ammoChange;
    }

    void Update() {

        if (Time.time > FireTime && IsFiring) {
            Fire();
        }

        if (Input.GetButtonDown("Fire1") && Time.time > FireTime && WeaponObject) {
            Fire();
            IsFiring = true;
        }

        if (Input.GetButtonUp("Fire1") && !CompleteBurst) {
            Reset();
        }

        if (Input.GetButtonDown("Reload") && !IsFiring && WeaponObject) {
            Reload();
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && !IsFiring && WeaponParent.transform.childCount > 1) {
            SelectedWeaponID++;

            if (SelectedWeaponID >= WeaponParent.transform.childCount) {
                SelectedWeaponID = 0;
            }

            SetWeapon(SelectedWeaponID);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && !IsFiring && WeaponParent.transform.childCount > 1) {
            SelectedWeaponID--;

            if (SelectedWeaponID < 0) {
                SelectedWeaponID = WeaponParent.transform.childCount - 1;
            }

            SetWeapon(SelectedWeaponID);
        }
    }

    public void SetWeapon(int ID) {
        int i = 0;
        foreach (Transform Weapon in WeaponParent.transform) {
            if (i == ID) {
                Weapon.GetComponent<WeaponObject>().Activate();
                WeaponObject = Weapon.gameObject;
                Sway.WeaponObject = Weapon.gameObject;
            }
            else {
                Weapon.GetComponent<WeaponObject>().Deactivate();
            }

            i++;
        }

        Reset();
        Sway.Deploy();
    }
}
