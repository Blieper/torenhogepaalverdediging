using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health {

    public bool CanRespawn = false;
    public float RespawnTime = 3f;

    CharacterMovement characterMovement;
    public GameObject WeaponParent;
    public GameObject[] Respawns;

    public override void Start() {
        base.Start();

        characterMovement = GetComponent<CharacterMovement>();
        Respawns = GameObject.FindGameObjectsWithTag("Respawn");
    }

    void Respawn() {
        GameObject respawn = Respawns[Random.Range(0, Respawns.Length)];
        this.transform.position = respawn.transform.position;

        characterMovement.canMove = true;
        WeaponParent.SetActive(true);
        Reset();
    }

    public override void Eliminate() {
        base.Eliminate();

        characterMovement.canMove = false;
        WeaponParent.SetActive(false);

        if (CanRespawn)
            Invoke("Respawn", RespawnTime);
    }

}
