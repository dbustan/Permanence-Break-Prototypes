using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : MonoBehaviour
{
    public LayerMask layerMask;
    public GameObject buttonTriggerObj;
    private ButtonTrigger buttonTrigger;

    private AudioSource [] offOnSounds;

    private int unpressedFrames;
    const int maxUnpressedFrames = 4;
    private bool colliding, pressed;

    void Start() {
        offOnSounds = GetComponents<AudioSource>();
        if(buttonTriggerObj) {
            buttonTrigger = buttonTriggerObj.GetComponent<ButtonTrigger>();
        }
        unpressedFrames = 0;
        colliding = false;
        pressed = false;
    }

    void FixedUpdate() {
        if(!colliding) {
            unpressedFrames++;
            if(pressed && unpressedFrames >= maxUnpressedFrames) {
                releaseButton();
            }
        } else {
            unpressedFrames = 0;
            if(!pressed) {
                pressButton();
            }
        }
        colliding = false;
    }
    private void OnTriggerStay(Collider other) {
        GameObject obj = other.gameObject;
        if(layerMask != (layerMask | (1 << obj.layer))) return;
        if(obj.GetComponent<VisibilityObject>() && obj.GetComponent<VisibilityObject>().phasedOut) return;
        colliding = true;
    }

    private void pressButton() {
        Debug.Log("PRESS");
        pressed = true;
        offOnSounds[0].Play();
        if(buttonTrigger != null) {
            
            buttonTrigger.buttonPress();
            
        }
    }
    private void releaseButton() {
        Debug.Log("RELEASE");
        pressed = false;
        offOnSounds[1].Play();
        if(buttonTrigger != null) {
            
            buttonTrigger.buttonRelease();
        }
    }
}

public interface ButtonTrigger {
    abstract void buttonPress();
    abstract void buttonRelease();
}
