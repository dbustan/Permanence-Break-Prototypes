using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanenceBlock : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource[] BounceSources;

    [SerializeField] 
    private GameObject BounceSFXobj; 

    private bool existing;

    
    private Rigidbody rb;
    void Start()
    {
        existing = true;
        rb = GetComponent<Rigidbody>();
        BounceSources = BounceSFXobj.GetComponents<AudioSource>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.name != "Player"){
            if (existing){
                int numRange = UnityEngine.Random.Range(0,3);
                BounceSources[numRange].Play();
            }
            
        }
        

    }

    private void OnCollisionExit(Collision other) {
        if(GetComponent<VisibilityObject>().phasedOut) {
            existing = false;
        } else {
            existing = true;
        }
    }
}
