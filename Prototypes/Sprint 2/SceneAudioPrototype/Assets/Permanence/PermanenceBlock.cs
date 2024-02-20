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

    
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        BounceSources = BounceSFXobj.GetComponents<AudioSource>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision other) {
        if (rb.velocity == Vector3.zero){
            return;
        }
        if (other.gameObject.name != "Player"){
            int numRange = UnityEngine.Random.Range(0,3);
            BounceSources[numRange].Play();
            
        }
        

    }
}
