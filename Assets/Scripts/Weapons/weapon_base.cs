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
    public float Weight = 25f;
    public float Recoil = 2f;

    public GameObject ProjectileType;
    public GameObject WeaponParent;
    public GameObject WeaponObject;
    public GameObject CameraObject;

    public int SelectedWeaponID = 0;

    Vector2 SwayDirSmooth = Vector2.zero;
    Vector2 SwayVector = Vector2.zero;
    Vector2 SwayVectorAccel = Vector2.zero;
    Vector2 SwayStep = Vector2.zero;

    character_movement charmove;

    float FireTime;
    public float FireDelay;
    int FiredBurst;
    bool IsFiring = false;

    void Start () {
        FireDelay = 1f / (FireRate / 60);
        charmove = transform.GetComponent<character_movement>();

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
            WeaponObject.GetComponent<Weapon_Object>().AmmoAmount--;
            SwayVector.y += Recoil;

            GameObject projectile = Instantiate(ProjectileType, CameraObject.transform.position, CameraObject.transform.rotation);
            Physics.IgnoreCollision(projectile.transform.GetComponent<Collider>(), transform.GetComponent<CharacterController>(), true);

            print("fire");
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

        if (Input.GetButtonUp("Fire1"))
        {
            Reset();
        }

        if (Input.GetButtonDown("Reload"))
        {
            Reload();
        }

        Vector2 swayDir = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        swayDir = swayDir.normalized * swayDir.magnitude;
        SwayDirSmooth = Vector2.Lerp(SwayDirSmooth, swayDir, 0.5f);
        SwayVector += SwayDirSmooth;
        SwayVector -= SwayVector * (4.015848f + (21.87705f - 4.015848f) / (1 + Mathf.Pow((Weight / 4.206759f), 1.480224f))) * Time.deltaTime;
     
        SwayVectorAccel += new Vector2(SwayVector.x > 0 ? -0.25f : 0.25f, SwayVector.y > 0 ? -0.25f : 0.25f) * Time.deltaTime;
        SwayVector += SwayVectorAccel * 200 * Time.deltaTime;

        Vector2 SwayVectorWeighted = SwayVector * ( Weight >= 5 ? 1 : -1);

        SwayStep.x += charmove.velocity.x * 2f * Time.deltaTime;
        SwayStep.y += charmove.velocity.z * 2f * Time.deltaTime;

        SwayVector.x += Mathf.Cos(SwayStep.magnitude);
        SwayVector.y += Mathf.Cos(SwayStep.magnitude * 2);

        SwayVector = SwayVector.normalized * Mathf.Clamp(SwayVector.magnitude, 0, 10);

        SwayVectorWeighted.x = Mathf.Clamp(SwayVectorWeighted.x, -10, 10);
        SwayVectorWeighted.y = Mathf.Clamp(SwayVectorWeighted.y, -10, 10);

        WeaponObject.transform.localRotation = Quaternion.Euler(SwayVectorWeighted.y, -SwayVectorWeighted.x, SwayVectorWeighted.x);

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            SelectedWeaponID++;

            if (SelectedWeaponID >= WeaponParent.transform.childCount)
            {
                SelectedWeaponID = 0;
            }

            SetWeapon(SelectedWeaponID);
            print(SelectedWeaponID);
        } else if (Input.GetAxis("Mouse ScrollWheel") < 0)
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
            }
            else
            {
                Weapon.GetComponent<Weapon_Object>().Deactivate();
            }

            i++;
        }

        Reset();
    }
}
