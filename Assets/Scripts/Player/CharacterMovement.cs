using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public Camera cam;
    public CharacterController controller;

    public float SprintSpeed = 5.0f;
    public float WalkSpeed = 3.0f;
    public float SpeedMul = 1f;
    public float Accelaration = 2.0f;
    public float Deccelaration = 2.0f;
    public float JumpHeight = 2.0f;
    public float FallMultiplier = 2.0f;
    public float CrouchHeight = 1.0f;

    public bool isJumping;
    public bool canMove = true;
    float LookAngle;
    float LeanAngle;
    float JumpSpeed;
    float CrouchValue;
    public Vector3 moveVector = Vector3.zero;
    public Vector3 accelarationVector = Vector3.zero;
    public Vector3 velocity = Vector3.zero;

    float GetAccelaration(float In, float Compare) {
        if (Mathf.Abs(In) > 0 && canMove) {
            return Mathf.Clamp(
                (In * (Input.GetButton("Sprint") ? SprintSpeed : WalkSpeed) * SpeedMul - Compare) * Accelaration,
                -Accelaration,
                Accelaration
            );
        }
        else {
            return Mathf.Clamp(
                (-Compare) * Deccelaration,
                -Deccelaration,
                Deccelaration
            );
        }
    }

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;

        JumpSpeed = Mathf.Sqrt(2 * -Physics.gravity.y * JumpHeight);

        controller = transform.GetComponent<CharacterController>();
    }

    void Update() {
        moveVector = Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));

        velocity.x += GetAccelaration(moveVector.x, velocity.x) * Time.deltaTime;
        velocity.z += GetAccelaration(moveVector.z, velocity.z) * Time.deltaTime;

        if (controller.isGrounded) {
            isJumping = false;

            if (Input.GetButtonDown("Jump") && canMove) {
                velocity.y = JumpSpeed;
                isJumping = true;
            }
        }
        else {
            velocity.y += Physics.gravity.y * Time.deltaTime;

            if (controller.velocity.y < 0 && controller.velocity.y > -JumpSpeed) {
                velocity.y += Physics.gravity.y * (FallMultiplier - 1) * Time.deltaTime;
            }
        }

        CrouchValue = Mathf.Lerp(CrouchValue, Input.GetButton("Crouch") ? CrouchHeight : 2f, 25f * Time.deltaTime);
        controller.height = CrouchValue;

        controller.Move(controller.transform.TransformVector(velocity * Time.deltaTime));

        controller.transform.RotateAround(controller.transform.position, Vector3.up, Input.GetAxis("Mouse X"));

        LookAngle -= Input.GetAxis("Mouse Y");

        LookAngle = Mathf.Clamp(LookAngle, -90, 90);

        LeanAngle += (-velocity.x / WalkSpeed - LeanAngle) * 4f * Time.deltaTime;

        cam.transform.localRotation = Quaternion.Euler(LookAngle, 0, LeanAngle);
    }
}
