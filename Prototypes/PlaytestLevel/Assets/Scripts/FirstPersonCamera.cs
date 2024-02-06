using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform player;

    public float mouseSensitivity = 5f;

    public float cameraVerticalRotation = 0;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        //scaling it by mouse sensitivity
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        //apply the rotation amount to from inputY to camera vertical rotation
        cameraVerticalRotation -= inputY;
        //clamp it so that it can only be -90 or 90
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
     
        //simply put, it transforms the vertical vector up or down depending, clamped at 90, -90
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;
        //rotates based on up and inputX;
        player.Rotate(Vector3.up * inputX);
    }
}
