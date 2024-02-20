using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float cameraSensitivity;
    public float verticalRange;
    bool leftLeg;
    private bool isPaused;
    public float speedRange;
    public float gravity, terminalVelocity;
    private float verticalVelocity;

    private float moveThreshold;

    private Vector3 LastPos;
    public float currentSpeed;
    public float speedChangeRate;
    private float currentSpeedPreCurve;
    public float jumpHeight;
    public float TimeSinceLastFootstep;
    public Image speedReadout;

    public GameObject LeftFoot, RightFoot;
    private Camera playerCamera;
    private bool jumping;
    private CharacterController cc;

    public GameObject PauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        leftLeg = true;
        isPaused = false;
        LastPos = transform.position;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerCamera = GetComponentInChildren<Camera>();
        cc = GetComponent<CharacterController>();
        speedReadout.rectTransform.localScale = new Vector3(1f, getSpeedReadoutScale(), 1f);
        currentSpeedPreCurve = Mathf.Sqrt(currentSpeed/speedRange);
        TimeSinceLastFootstep = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
        updateSpeed();
        if (!isPaused) {
            cameraRotation();
             move();
        }      
        checkPause();
        if(!cc.isGrounded) {
            verticalVelocity = Mathf.Clamp(verticalVelocity - gravity * Time.deltaTime, -terminalVelocity, 1000);
        } else {
            if(!jumping) {
                if(Input.GetMouseButtonDown(2)) {
                    verticalVelocity = Mathf.Sqrt(2*gravity*jumpHeight);
                    jumping = true;
                }
            } else {
                verticalVelocity = 0f;
            }
            if(jumping && verticalVelocity <= 0) {
                jumping = false;
            }
        }
    }
    private void PlayFootStep(){
        int index = UnityEngine.Random.Range(0, 1);
        if (leftLeg) {
            AudioSource[] stepSounds = LeftFoot.GetComponents<AudioSource>();
            stepSounds[index].Play();
            
        } else {
            AudioSource[] stepSounds = RightFoot.GetComponents<AudioSource>();
            stepSounds[index].Play();
           
        }
        
        leftLeg = !leftLeg;
        //Alternates Leg and Shuffles its choice to play

    }
    private void checkPause(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            Cursor.visible = !Cursor.visible;
            if (Cursor.lockState == CursorLockMode.Locked) {
                Cursor.lockState = CursorLockMode.None;
            } else {
                Cursor.lockState = CursorLockMode.Locked;
            }
            
            PauseMenu.SetActive(!PauseMenu.activeSelf);
            isPaused = !isPaused;
        }
    }
    private void move() {
        moveThreshold = 1.45f;
        float forward = Input.GetMouseButton(1) ? 1f : 0f;
        Vector3 movement = transform.rotation * new Vector3(0f, 0f, forward) * currentSpeed * Time.deltaTime;
        movement += Vector3.up * verticalVelocity * Time.deltaTime;
        float moveDistance = Vector3.Distance(transform.position, LastPos);
        if (moveDistance >= moveThreshold) {
            LastPos = transform.position;
            PlayFootStep();
        } 
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

    private void updateSpeed() {
        currentSpeedPreCurve = Mathf.Clamp(currentSpeedPreCurve + Input.mouseScrollDelta.y * speedChangeRate, -1, 1);
        currentSpeed = speedRange * Mathf.Pow(currentSpeedPreCurve, 2) * Math.Sign(currentSpeedPreCurve);
        speedReadout.rectTransform.localScale = new Vector3(1f, getSpeedReadoutScale(), 1f);
    }
    private float getSpeedReadoutScale() {
        return 6*(currentSpeed/speedRange);
    }
}
