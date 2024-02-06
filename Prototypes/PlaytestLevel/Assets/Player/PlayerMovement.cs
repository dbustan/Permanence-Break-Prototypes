using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float cameraSensitivity;
    public float verticalRange;

    public float minSpeed, maxSpeed;
    public float gravity, terminalVelocity;
    private float verticalVelocity;
    public float currentSpeed;

    private Camera playerCamera;
    private CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerCamera = GetComponentInChildren<Camera>();
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = Mathf.Clamp(currentSpeed + Input.mouseScrollDelta.y, minSpeed, maxSpeed);
        cameraRotation();
        move();
        if(!cc.isGrounded) {
            verticalVelocity = Mathf.Clamp(verticalVelocity - gravity * Time.deltaTime, -terminalVelocity, 1000);
        } else {
            verticalVelocity = 0f;
        }
    }

    private void move() {
        float forward = Input.GetMouseButton(1) ? 1f : 0f;

        Vector3 movement = transform.rotation * new Vector3(0f, 0f, forward) * currentSpeed * Time.deltaTime;
        movement += Vector3.up * verticalVelocity * Time.deltaTime;
        cc.Move(movement);
    }
    private void cameraRotation() {
        Vector2 mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 rotationDelta = new Vector3(-mouseMovement.y, mouseMovement.x, 0f) * cameraSensitivity * Time.deltaTime;

        float currentPlayerRotationY = transform.localRotation.eulerAngles.y;
        currentPlayerRotationY += rotationDelta.y;
        transform.localRotation = Quaternion.Euler(Vector3.up * currentPlayerRotationY);

        float currentCameraRotationX = playerCamera.transform.localRotation.eulerAngles.x;
        if(currentCameraRotationX >= 180) currentCameraRotationX -= 360;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX + rotationDelta.x, -verticalRange/2, verticalRange/2);
        playerCamera.transform.localRotation = Quaternion.Euler(Vector3.right * currentCameraRotationX);
    }
}
