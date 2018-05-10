using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attributes : MonoBehaviour {

    CharacterMovement characterMovement;
    public GameObject WeaponParent;
    public GameObject[] Respawns;

    public int MaxHealth = 100;
    private int health;
    public int Health {
        get {
            return health;
        }
        set {
            health = value;

            health = Mathf.Clamp(health, 0, MaxHealth);

            if (health == 0) {
                Eliminate();
            }
        }
    }

    private float speed;
    public float Speed {
        get {
            return speed;
        }
        set {
            speed = value;
            //characterMovement.SpeedMul = value;
        }
    }

    void Start() {
        characterMovement = GetComponent<CharacterMovement>();
        print(characterMovement);

        Speed = 1f;
        Health = MaxHealth;

        Respawns = GameObject.FindGameObjectsWithTag("Respawn");
    }

    void Eliminate () {
        characterMovement.canMove = false;
        WeaponParent.SetActive(false);

        Invoke("Respawn", 3f);
    }

    void Respawn () {
        GameObject respawn = Respawns[Random.Range(0, Respawns.Length)];
        this.transform.position = respawn.transform.position;

        characterMovement.canMove = true;
        WeaponParent.SetActive(true);
        Health = MaxHealth;
    }
}
