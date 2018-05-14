using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInteractable : Interactable {

    public override void Interact(GameObject Object)
    {
        GetComponent<WeaponPickup>().OnPickup(Object, true);
    }
}
