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
    public int AmmoCapacity = 30;
    public int ReserveAmmo = 90;
    public float ReloadTime = 2f;
    public int AmmoAmount = 0;

    public Projectile ProjectileType;
    public Mesh WeaponMesh;

    float FireTime;
    float FireDelay;
    int FiredBurst;
    bool IsFiring = false;

    void Start () {
        FireDelay = 1f / (FireRate / 60);
        AmmoAmount = AmmoCapacity;
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

        if (AmmoAmount > 0)
        {
            AmmoAmount--;
            print("Test");
        }

        CheckBurst();
    }

    void Reload ()
    {
        if (AmmoAmount < AmmoCapacity && ReserveAmmo > 0) {
            Invoke("AfterReload", ReloadTime);
        }
    }

    void AfterReload ()
    {
        Reset();
        int ammoChange = AmmoCapacity - AmmoAmount;
        ReserveAmmo -= ammoChange;
        AmmoAmount += ammoChange;
    }
	
	void Update () {

        if (Time.time > FireTime && IsFiring)
        {
            Fire();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
            IsFiring = true;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            Reset();
        }

        if (Input.GetButtonDown("Reload"))
        {
            Reload();
        }
	}
}
