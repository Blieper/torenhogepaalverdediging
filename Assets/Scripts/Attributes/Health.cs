using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Statistic {

    public bool DestroyWhenEmpty = false;

    public override void Start () {
        base.Start();
    }

    public override void OnValueChanged(float Old, float New) {
        base.OnValueChanged(Old, New);

        print(New);

        if (Value == 0) {
            Eliminate();
        }
    }

    public virtual void Eliminate() {
        if (DestroyWhenEmpty) {
            Destroy(gameObject);
        } 
    }
}
