using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Plasma : Projectile {
    public float ExplosionRadius = 5.0F;
    public float ExplosionPower = 10.0F;
    public float Velocity = 500f;
    public int ExplosionDamage = 15;

    void Explode() {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, ExplosionRadius);
        foreach (Collider hit in colliders) {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(ExplosionPower, explosionPos, ExplosionRadius, 0);

            Health healthStat = hit.GetComponent<Health>();

            if (healthStat != null)
                healthStat.Value -= ExplosionDamage;
        }

        Destroy(gameObject);
    }

    public override void Tick() {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Velocity * Time.deltaTime, layerMask)) {
            transform.position = hit.point;
            OnHit();
        }
        else {
            transform.position += transform.TransformDirection(Vector3.forward) * Velocity * Time.deltaTime;
        }
    }

    public override void OnHit() {
        Explode();
    }
}

