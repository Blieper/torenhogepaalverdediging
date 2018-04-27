using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Bomb : Projectile {

	// Use this for initialization
	public override void Fire() {
        rb.velocity  = transform.TransformVector(new Vector3(0,0,100));
    }
}
