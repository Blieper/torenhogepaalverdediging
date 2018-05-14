using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour {

    Vector2 SwayDirSmooth = Vector2.zero;
    Vector2 SwayVector = Vector2.zero;
    Vector2 SwayVectorAccel = Vector2.zero;
    Vector2 SwayVectorAccelDir = Vector2.zero;
    Vector2 SwayVectorAccelDirSmooth = Vector2.zero;
    float SwayStep;
    CharacterMovement charmove;
    float SwitchOffset;

    public GameObject WeaponObject;
    public GameObject WeaponParent;
    public float Weight;
    public float Lateral = 1f;
    public float LateralMax;

    public void Impulse(Vector2 Dir) {
        SwayVector += Dir * -Mathf.Sign(Weight);
    }

    public void Deploy() {
        SwitchOffset = 90f;
    }

    void Start() {
        charmove = transform.GetComponent<CharacterMovement>();
    }

    void Update() {
        Vector2 swayDir = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        swayDir = swayDir.normalized * swayDir.magnitude;
        SwayDirSmooth = Vector2.Lerp(SwayDirSmooth, swayDir, 15f * Time.deltaTime);
        SwayVector += SwayDirSmooth;
        SwayVector -=
            SwayVector *
            (4.015848f + (21.87705f - 4.015848f) / (1 + Mathf.Pow((Mathf.Abs(Weight) / 4.206759f), 1.480224f)))
            * Time.deltaTime;

        SwayVectorAccelDir = new Vector2(SwayVector.x > 0 ? -0.25f : 0.25f, SwayVector.y > 0 ? -0.25f : 0.25f);
        SwayVectorAccelDirSmooth = Vector2.Lerp(SwayVectorAccelDirSmooth, SwayVectorAccelDir, 1f * Time.deltaTime);

        SwayVectorAccel += SwayVectorAccelDirSmooth * Time.deltaTime;
        SwayVector += SwayVectorAccel * 200 * Time.deltaTime;

        Vector2 SwayVectorWeighted = SwayVector * Mathf.Sign(Weight);

        SwayStep += new Vector3(charmove.velocity.x, 0, charmove.velocity.z).magnitude * Time.deltaTime * 2f;

        SwayVector.x += Mathf.Cos(SwayStep) * Time.deltaTime * 25f;
        SwayVector.y += Mathf.Cos(SwayStep * 2) * Time.deltaTime * 25f;

        SwayVector.x += (Mathf.PerlinNoise(Time.time * 0.5f, 0) - 0.5f) * 2f / Mathf.Abs(Weight);
        SwayVector.y += (Mathf.PerlinNoise(Time.time * 0.5f, 10) - 0.5f) * 2f / Mathf.Abs(Weight);

        SwayVector = SwayVector.normalized * Mathf.Clamp(SwayVector.magnitude, 0, 10);

        SwayVectorWeighted.x = Mathf.Clamp(SwayVectorWeighted.x, -10, 10);
        SwayVectorWeighted.y = Mathf.Clamp(SwayVectorWeighted.y, -10, 10);

        if (charmove.isJumping) {
            SwayVector.y += charmove.velocity.y * 0.25f * Mathf.Sign(Weight);
        }

        if (SwitchOffset > 0.1f) {
            SwitchOffset = Mathf.Lerp(SwitchOffset, 0, Time.deltaTime * 10f);
        }

        if (WeaponObject) {
            WeaponObject.transform.localRotation = Quaternion.Euler(
                                                        SwayVectorWeighted.y - SwitchOffset,
                                                        -SwayVectorWeighted.x - SwitchOffset,
                                                        SwayVectorWeighted.x);
        }

        WeaponParent.transform.localPosition = new Vector3(
                                                    SwayVectorWeighted.x - SwitchOffset,
                                                    -SwayVectorWeighted.y - SwitchOffset,
                                                    SwayVectorWeighted.x)
                                                * Lateral / 100;


    }
}
