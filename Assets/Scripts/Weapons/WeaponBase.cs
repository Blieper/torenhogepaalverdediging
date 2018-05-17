using UnityEngine;
using UnityEngine.Networking;

public class WeaponBase : NetworkBehaviour {

    // How many rounds per minute does the weapon fire?
    [SyncVar] public float FireRate = 500;
    // How many bullets does the weapon fire per trigger-pull?
    // 0 = fully auto, 1 = semi auto, 2 = 2 round burst etc
    [SyncVar] public int Burst = 0;
    [SyncVar] public bool CompleteBurst = false;
    [SyncVar] public float ReloadTime = 2f;
    [SyncVar] public float Recoil = 2f;

    [SyncVar] public GameObject ProjectileType;
    [SyncVar] public GameObject WeaponParent;
    [SyncVar] public GameObject WeaponObject;
    [SyncVar] public GameObject CameraObject;
    [SyncVar] public GameObject Muzzle;

    [SyncVar] public int SelectedWeaponID = 0;

    CharacterMovement charmove;
    public WeaponSway Sway;

    float FireTime;
    public float FireDelay;
    int FiredBurst;
    bool IsFiring = false;

    public void OnStartServer() {
        FireDelay = 1f / (FireRate / 60);
        charmove = transform.GetComponent<CharacterMovement>();
        Sway = transform.GetComponent<WeaponSway>();

        CmdSetWeapon(0);
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

    [Command]
    void CmdFire() {
        if (!IsFiring) {
            return;
        }

        if (WeaponObject.GetComponent<WeaponObject>().AmmoAmount > 0) {
            Sway.Impulse(new Vector2(Random.value * 2 - 1, Recoil));

            WeaponObject.GetComponent<WeaponObject>().AmmoAmount--;

            GameObject projectile = Instantiate(ProjectileType, CameraObject.transform.position, CameraObject.transform.rotation);
            projectile.GetComponent<Projectile>().Initialise(gameObject, Muzzle.transform.position, CameraObject.transform.position);

            NetworkServer.Spawn(projectile);
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
            CmdFire();
        }

        if (Input.GetButtonDown("Fire1") && Time.time > FireTime && WeaponObject) {
            CmdFire();
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

            CmdSetWeapon(SelectedWeaponID);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && !IsFiring && WeaponParent.transform.childCount > 1) {
            SelectedWeaponID--;

            if (SelectedWeaponID < 0) {
                SelectedWeaponID = WeaponParent.transform.childCount - 1;
            }

            CmdSetWeapon(SelectedWeaponID);
        }
    }

    [Command]
    public void CmdSetWeapon(int ID) {
        int i = 0;
        foreach (Transform Weapon in WeaponParent.transform) {
            if (i == ID) {
                Weapon.GetComponent<WeaponObject>().Activate();
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
