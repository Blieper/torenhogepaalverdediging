using UnityEngine;
using UnityEngine.Networking;

public class WeaponObject : NetworkBehaviour {

    // How many rounds per minute does the weapon fire?
    [SyncVar] public float FireRate = 500;
    // How many bullets does the weapon fire per trigger-pull?
    // 0 = fully auto, 1 = semi auto, 2 = 2 round burst etc
    [SyncVar] public int Burst = 0;
    [SyncVar] public bool CompleteBurst = false;
    [SyncVar] public int AmmoCapacity = 30;
    [SyncVar] public int ReserveAmmo = 90;
    [SyncVar] public float ReloadTime = 2f;
    [SyncVar] public int AmmoAmount = 0;
    [SyncVar] public float SwayWeight = 25f;
    [SyncVar] public float Recoil = 2f;
    [SyncVar] public float SpeedMul = 1f;

    public Vector3 Offset = new Vector3(0, 0, 0);
    [SyncVar] public NetworkInstanceId OwnerNetID;
    public GameObject ProjectileType;
    public GameObject PickupObject;
    public GameObject Muzzle;
    public Attributes Attributes;

    public override void OnStartServer() {
        AmmoAmount = AmmoCapacity;
    }

    public void Activate() {
        gameObject.SetActive(true);

        WeaponBase WeaponBase = NetworkServer.FindLocalObject(OwnerNetID).GetComponent<WeaponBase>();
        WeaponBase.FireRate = FireRate;
        WeaponBase.Burst = Burst;
        WeaponBase.CompleteBurst = CompleteBurst;
        WeaponBase.ReloadTime = ReloadTime;
        WeaponBase.Recoil = Recoil;
        WeaponBase.FireDelay = 1f / (FireRate / 60);
        WeaponBase.Sway.Weight = SwayWeight;
        WeaponBase.Sway.WeaponObject = WeaponBase.WeaponObject = NetworkServer.FindLocalObject(netId);
        WeaponBase.Muzzle = Muzzle;
        WeaponBase.ProjectileType = ProjectileType;
    }

    public void Deactivate() {
        gameObject.SetActive(false);
    }
}
