using UnityEngine;
using UnityEngine.Networking;

public class Pickup : NetworkBehaviour
{

    public GameObject VisualPart;

    float Rotation;
    float OscillationHeight = 0.1f;

    void Update()
    {
        Rotation += 30 * Time.deltaTime;

        VisualPart.transform.rotation = Quaternion.Euler(0, Rotation, 0);

        VisualPart.transform.localPosition = new Vector3(0,Mathf.Sin(Rotation * 0.1f) * OscillationHeight, 0);
    }

    public virtual void OnPickup(GameObject Object, bool Interacted = false)
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (!isServer)
            return;

        OnPickup(collider.gameObject);
    }
}
