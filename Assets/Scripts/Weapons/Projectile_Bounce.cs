using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Bounce : Projectile
{
    public float Velocity = 500f;
    public int Damage = 15;

    Vector3 MoveDirection = Vector3.zero;

    public override void Fire()
    {
        MoveDirection = transform.forward;
    }

    public override void Tick()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Velocity * Time.deltaTime, layerMask))
        {
            Health healthStat = hit.collider.GetComponent<Health>();

            if (healthStat != null)
            {
                healthStat.Value -= Damage;
                //GameObject.Destroy(gameObject);
            } 

            transform.position = hit.point;
            MoveDirection = Vector3.Reflect(MoveDirection, hit.normal);
            transform.position += MoveDirection * Velocity * Time.deltaTime;
        }
        else
        {
            transform.position += MoveDirection * Velocity * Time.deltaTime;
        }
    }
}

