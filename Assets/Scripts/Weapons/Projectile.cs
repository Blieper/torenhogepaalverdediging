using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public GameObject effect;
    public Vector3 Offset;
    public Vector3 Start;
    public Vector3 Muzzle;
    public float Lifetime = 5f;

    public void Initialise(GameObject Instigator, Vector3 MuzzlePos, Vector3 ShootPos) {
        Start = ShootPos;
        Muzzle = MuzzlePos;

        if (transform.GetComponent<Collider>()) {
            Physics.IgnoreCollision(transform.GetComponent<Collider>(), Instigator.transform.GetComponent<CharacterController>(), true);
        }

        Offset = transform.InverseTransformVector(MuzzlePos - ShootPos);

        if (effect)
            effect.transform.localPosition = Offset;

        Fire();
        Invoke("AutoDestroy", Lifetime);
    }

    void Update() {
        Offset = Vector3.Lerp(Offset, Vector3.zero, 10f * Time.deltaTime);

        if (effect)
            effect.transform.localPosition = Offset;

        Tick();
    }
    public virtual void Fire() { }
    public virtual void Tick() { }
    public virtual void OnHit() {
        Offset = Vector3.zero;
    }

    void AutoDestroy() {
        Destroy(gameObject);
    }
}
