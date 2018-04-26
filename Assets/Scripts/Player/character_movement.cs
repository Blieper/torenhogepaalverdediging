using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_movement : MonoBehaviour {

    public Camera cam;
    public CharacterController controller;

    public float WalkSpeed = 3.0f;
    public float Accelaration = 2.0f;
    public float Deccelaration = 2.0f;
    public float JumpHeight = 2.0f;

    float LookAngle;
    float LeanAngle;
    float JumpSpeed;
    public Vector3 moveVector = Vector3.zero;
    public Vector3 accelarationVector = Vector3.zero;
    public Vector3 velocity = Vector3.zero;

    float GetAccelaration (float Input, float Compare)
    {
        if (Mathf.Abs(Input) > 0){
            return Mathf.Clamp(
                (Mathf.Sign(Input) * WalkSpeed - Compare) * Accelaration,
                -Accelaration,
                Accelaration
            );
        } else {
            return Mathf.Clamp(
                (-Compare) * Deccelaration,
                -Deccelaration,
                Deccelaration
            ) ;
        }
    }

	void Start () {
        Cursor.lockState = CursorLockMode.Locked;

        JumpSpeed = Mathf.Sqrt(2 * -Physics.gravity.y * JumpHeight);

        controller = transform.GetComponent<CharacterController>();
    }
	
	void Update () {
        moveVector = Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")));

        velocity.x += GetAccelaration(moveVector.x, velocity.x) * Time.deltaTime;
        velocity.z += GetAccelaration(moveVector.z, velocity.z) * Time.deltaTime;

        if (controller.isGrounded) {
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = JumpSpeed;
            }
        } else {
            velocity.y += Physics.gravity.y * Time.deltaTime;
        }

        controller.Move(controller.transform.TransformVector(velocity * Time.deltaTime));

        controller.transform.RotateAround(controller.transform.position, Vector3.up, Input.GetAxis("Mouse X"));

        LookAngle -= Input.GetAxis("Mouse Y");

        LookAngle = Mathf.Clamp(LookAngle,-90,90);

        LeanAngle += (-velocity.x/WalkSpeed - LeanAngle) * 4f * Time.deltaTime;

        cam.transform.localRotation = Quaternion.Euler(LookAngle, 0, LeanAngle);
    }
}
