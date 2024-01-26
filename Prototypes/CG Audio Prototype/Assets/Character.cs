using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour
{
    public float speed;

    public float jumpHeight;

    private float yVelocity;

    public Transform cameraTransform;
    private CharacterController characterController;
    private Vector3 direction;

    private Vector3 velocity;

    private const float gravity = 9.8f;
    private bool jumping;

    
    
    void Start()
    {
        jumping = false;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {   
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 forward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;
        direction = forward * verticalInput + right * horizontalInput;
        velocity = direction * speed;
        if (characterController.isGrounded && !jumping){
            yVelocity = -1f;
            if (Input.GetKeyDown(KeyCode.Space)){
                yVelocity = Mathf.Sqrt(2*gravity*jumpHeight);
                jumping = true;
            }
        } else {
            yVelocity -= gravity * Time.deltaTime;
            if (yVelocity <= 0) {
                jumping = false;
            }
        }
        velocity.y = yVelocity;
        characterController.Move(velocity * Time.deltaTime);
        InputCheck();
    
    }
    private void InputCheck()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            int layerMask = 1 << 6;
            RaycastHit hit;

            if (Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {
                Debug.Log("Did hit");
            } else
            {
                Debug.Log("NOT");
            }

        }
    }
}


