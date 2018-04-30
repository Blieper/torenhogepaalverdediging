﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Plasma : Projectile
{
    public float ExplosionRadius = 5.0F;
    public float ExplosionPower = 10.0F;

    // Use this for initialization
    public override void Fire()
    {
        rb.velocity = transform.TransformVector(new Vector3(0, 0, 1000));
    }

    void Explode()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, ExplosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            print(rb);

            if (rb != null)
                rb.AddExplosionForce(ExplosionPower, explosionPos, ExplosionRadius, 3F);
        }

        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        print(collision);
        Explode();
    }
}

