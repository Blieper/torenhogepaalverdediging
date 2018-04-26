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
    public float Weight = 25f;

    public Projectile ProjectileType;
    public Mesh WeaponMesh;
    public GameObject WeaponObject;

    Vector2 SwayDirSmooth = Vector2.zero;
    Vector2 SwayVector = Vector2.zero;
    Vector2 SwayVectorAccel = Vector2.zero;
    Vector2 SwayStep = Vector2.zero;

    character_movement charmove;

    float FireTime;
    float FireDelay;
    int FiredBurst;
    bool IsFiring = false;

    void Start () {
        FireDelay = 1f / (FireRate / 60);
        AmmoAmount = AmmoCapacity;

        charmove = transform.GetComponent<character_movement>();
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

        SwayVector.x += Mathf.Cos(SwayStep.x);
        SwayVector.x += Mathf.Cos(SwayStep.y);

        WeaponObject.transform.localRotation = Quaternion.Euler(SwayVectorWeighted.y, -SwayVectorWeighted.x, SwayVectorWeighted.x);
    }
}
