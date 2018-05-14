using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway2 : MonoBehaviour {

    public float Lag = 0.5f;
    public float Damping = 0.75f;
    public float Smoothing = 0.5f;

    public GameObject ViewCamera;
    public GameObject WeaponObject;
    public GameObject WeaponParent;

    private Quaternion currentRot;
    private Quaternion targetRot;
    private Quaternion velocityRot;
    private Quaternion velocityDelta;

    private void Start() {
        currentRot = ViewCamera.transform.rotation;
        targetRot = ViewCamera.transform.rotation;
        velocityRot = targetRot;
    }

    // Update is called once per frame
    void Update() {

        targetRot = ViewCamera.transform.rotation;

        velocityDelta = Quaternion.RotateTowards(currentRot, targetRot, 5f);

        velocityRot = Quaternion.RotateTowards(velocityRot, velocityDelta * velocityRot, 5);
        velocityRot = Quaternion.Lerp(velocityRot, new Quaternion(0, 0, 0, 0), Damping);

        currentRot = Quaternion.RotateTowards(currentRot, velocityRot * currentRot, 5);

        print(velocityRot);

        WeaponParent.transform.rotation = currentRot;
    }
}
