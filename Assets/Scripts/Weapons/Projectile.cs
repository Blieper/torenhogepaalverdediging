using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = transform.GetComponent<Rigidbody>();
        Invoke("AutoDestroy",5f);

        Fire();
    }

    public virtual void Fire ()
    {

    }

    void AutoDestroy ()
    {
        Destroy(gameObject);
    }
}
