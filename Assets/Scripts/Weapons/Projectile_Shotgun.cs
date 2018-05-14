using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Shotgun : Projectile {
    public float Spread = 1.0F;
    public int Amount = 12;
    public GameObject ProjectileType;

    public override void Fire() {
        for (int i = 1; i <= Amount; i++) {

            Vector2 Spreadvec = new Vector2(Random.value, Random.value);
            Spreadvec = Spreadvec.normalized * Random.Range(0, Spread);

            GameObject projectile = Instantiate(
                ProjectileType,
                transform.position,
                Quaternion.LookRotation(
                    transform.forward + transform.right * Spreadvec.x + transform.up * Spreadvec.y,
                    transform.up
                    ));

            projectile.GetComponent<Projectile>().Initialise(gameObject, Muzzle, Start);
        }
    }
}
