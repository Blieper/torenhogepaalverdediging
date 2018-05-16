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
    [SyncVar] public GameObject ProjectileType;
    [SyncVar] public GameObject PickupObject;
    [SyncVar] public GameObject Owner;
    public GameObject Muzzle;
    public WeaponBase WeaponBase;
    public Attributes Attributes;

    public override void OnStartServer() {
        AmmoAmount = AmmoCapacity;
    }

    public void SetOwner(GameObject NewOwner, NetworkInstanceId NetID) {
        Owner = NewOwner;
        CmdDeactivate();

        RpcSetOwner(NetID);
    }

    [ClientRpc]
    public void RpcSetOwner (NetworkInstanceId NetID) {
        Owner = ClientScene.FindLocalObject(NetID);
        transform.SetParent(Owner.GetComponent<WeaponBase>().WeaponParent.transform);
        transform.localPosition = Offset;

        int WeaponCount = Owner.GetComponent<WeaponBase>().WeaponParent.transform.childCount;


        if (WeaponCount == 1) {
            WeaponBase = Owner.GetComponent<WeaponBase>();
            WeaponBase.CmdSetWeapon(0);
        }
    }

    public void Activate() {
        gameObject.SetActive(true);

        WeaponBase = Owner.GetComponent<WeaponBase>();
        WeaponBase.FireRate = FireRate;
        WeaponBase.Burst = Burst;
        WeaponBase.CompleteBurst = CompleteBurst;
        WeaponBase.ReloadTime = ReloadTime;
        WeaponBase.Recoil = Recoil;
        WeaponBase.FireDelay = 1f / (FireRate / 60);
        WeaponBase.Sway.Weight = SwayWeight;
        WeaponBase.WeaponObjectNetID = netId;
    }

    [Command]
    public void CmdDeactivate() {
        gameObject.SetActive(false);
    }
}
