using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Interactible : MonoBehaviour
{
    public string interactionInfoText;
    public Vector3 interactionTextOffset;

    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    public void grab() {
        if(GetComponent<VisibilityObject>()) {
            GetComponent<VisibilityObject>().grab();
        }
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.freezeRotation = true;
    }
    public void drop() {
        rb.useGravity = true;
        rb.freezeRotation = false;
        if(GetComponent<VisibilityObject>()) {
            GetComponent<VisibilityObject>().drop();
        }
    }

    public Vector3 getInteractionTextPosition() {
        return transform.position + interactionTextOffset;
    }
}
