using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Bomb : Projectile {
    public float ExplosionRadius = 5.0F;
    public float ExplosionPower = 10.0F;
    public float ExplosionTime = 2F;
    public int ExplosionDamage = 25;

    public Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    public override void Fire() {
        rb.velocity = transform.TransformVector(new Vector3(0, 0, 25));

        Invoke("Explode", ExplosionTime);
    }

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

    void OnCollisionEnter(Collision collision) {
        OnHit();
    }
}
