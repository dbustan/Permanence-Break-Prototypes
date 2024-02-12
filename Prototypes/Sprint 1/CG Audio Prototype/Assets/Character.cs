using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour
{
    public float speed;

    public float jumpHeight;

    private float yVelocity;

    private bool isMoving;


    public Transform cameraTransform;
    private CharacterController characterController;
    private Vector3 direction;

    private Vector3 velocity;

    [SerializeField] private SoundManager sm;

    private const float gravity = 9.8f;
    private bool jumping;
    private float timeSinceLastFootstep;


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
        if (direction != Vector3.zero)
        {
            isMoving = true;
        } else
        {
            isMoving = false;
        }
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
        velocity = new Vector3(0, 0, 0);

        InputCheck();
        FootstepsCheck(isMoving);
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
                Spawner buttonScript = hit.collider.gameObject.GetComponent<Spawner>();
                buttonScript.RandomPos();

            } else
            {
                Debug.Log("NOT");
            }

        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            speed++;
        } else if (scroll < 0f)
        {
            speed--;
        } else
        {
            Debug.Log("no speed change");
        }
    }
    private void FootstepsCheck(bool isMoving)
    {
        if (!isMoving || !characterController.isGrounded)
        {
            return;
        }
        //The bigger the speed, the smaller the value, the faster the audio plays
        if (Time.time - timeSinceLastFootstep >= 1f / speed)
        {
            sm.PlayerWalk();
            timeSinceLastFootstep = Time.time;
        }
    }
}



