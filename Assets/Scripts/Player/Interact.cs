using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {

    public GameObject Camera;

	void Update () {
		if (Input.GetButtonDown("Interact"))
        {
            // Bit shift the index of the layer (8) to get a bit mask
            int layerMask = 1 << 8;

            // This would cast rays only against colliders in layer 8.
            // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
            layerMask = ~layerMask;

            RaycastHit hit;
            if (Physics.Raycast(Camera.transform.position,Camera.transform.forward,out hit,3f, layerMask))
            {
                Interactable interactable = hit.collider.gameObject.GetComponent<Interactable>();

                if (interactable)
                {
                    interactable.Interact(gameObject);
                }
            }
        }
	}
}
