using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityObject : MonoBehaviour
{
    public int objId;

    public bool visible;
    public bool phasedOut;
    public bool grabbed;

    public Renderer mainRenderer;

    private int unseenFrames;
    private const int MAX_UNSEEN_FRAMES = 5;
    protected Rigidbody rb;
    protected Vector3 phaseOutVelocity, phaseOutAngularVelocity;

    // Start is called before the first frame update
    void Start()
    {
        unseenFrames = 0;
        visible = false;
        rb = GetComponent<Rigidbody>();
        phaseOutVelocity = Vector3.zero;
        phaseOutAngularVelocity = Vector3.zero;
        phasedOut = false;
    }

    public void setObjectId(int idInt) {
        objId = idInt;
    }

    public bool incrementUnseenFrames() {
        if(!visible) return true;
        unseenFrames++;
        if(unseenFrames >= MAX_UNSEEN_FRAMES) {
            setHidden();
            return true;
        }
        return false;
    }
    public virtual void setVisible() {
        visible = true;
        unseenFrames = 0;
    }
    public virtual void setHidden() {
        visible = false;
    }

    public virtual void grab() {
        grabbed = true;
        phaseIn();
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    public virtual void drop() {
        grabbed = false;
        phaseOutVelocity = rb.velocity;
        phaseOutAngularVelocity = rb.angularVelocity;
    }

    protected void phaseOut() {
        phasedOut = true;
        if(rb) {
            rb.useGravity = false;
            phaseOutVelocity = rb.velocity;
            phaseOutAngularVelocity = rb.angularVelocity;
            rb.isKinematic = true;
        }
    }
    protected void phaseIn() {
        phasedOut = false;
        if(rb) {
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.velocity = phaseOutVelocity;
            rb.angularVelocity = phaseOutAngularVelocity;
        }
    }
}
