using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class audioBuildup : MonoBehaviour
{
    [SerializeField]
   private AudioSource fallingSound;
    private BoxCollider bc;
    public float maxVol = 1f;
    public float maxSpeed = 10f;
    private bool fallingSoundPlayed;
    void Start()
    {
        fallingSoundPlayed = false;
        bc = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //float speedPerc = rb.velocity.magnitude / maxSpeed;

        //float newVol = Mathf.Lerp(0f, maxVol, speedPerc);

        //if (rb.velocity.magnitude == 0 && !fallingSoundPlayed)
        //{
        //Debug.Log(newVol);
        //fallingSoundPlayed = true;
        //fallingSound.volume = newVol;
        //fallingSound.Play();
        //}
        


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!fallingSoundPlayed)
        {
            fallingSound.Play();
            fallingSoundPlayed = true;
        }
    }
}
