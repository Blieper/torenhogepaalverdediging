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
	
	// Update is called once per frame
	void Update () {
		
	}

    void AutoDestroy ()
    {
        Destroy(gameObject);
    }
}
