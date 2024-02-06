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

   


    public Transform cameraTransform;
    private CharacterController characterController;

    [SerializeField]
    private GameObject Camera;


    private Vector3 velocity;




    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {   
        //characterController.Move(velocity * Time.deltaTime);
        InputCheck();
        MovementCheck();
    }
    private void InputCheck()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
           //For you Gabriel!
        }
        MovementCheck();
    }
    private void MovementCheck()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            speed++;
        }
        else if (scroll < 0f)
        {
            if (speed < 0f)
            {
                speed = 0f;
            }
            else
            {
                speed--;
            }
            
        }
        else
        {
            Debug.Log("no speed change");
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = transform.forward;
            velocity = direction * speed;
            characterController.Move(velocity * Time.deltaTime);
        }
    }
    
}



