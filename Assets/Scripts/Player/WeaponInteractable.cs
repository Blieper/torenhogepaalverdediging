using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInteractable : Interactable {

    public override void Interact()
    {
        GetComponent<WeaponPickup>().OnPickup();
    }
}
