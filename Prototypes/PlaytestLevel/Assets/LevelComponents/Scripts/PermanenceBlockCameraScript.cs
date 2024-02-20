using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanenceBlockCameraScript : MonoBehaviour
{
    public Transform[] armatures;
    public float minimumExtension, maximumExtension, overshootAmmount, speed;

    private List<float>[] camTimeTargets;
    private float[] animationStartTimes;

    private float[] timeTillAdd;
    
    void Start()
    {
        camTimeTargets = new List<float>[armatures.Length];
        animationStartTimes = new float[armatures.Length];
        timeTillAdd = new float[armatures.Length];
        for(int i = 0; i < armatures.Length; i++) {
            camTimeTargets[i] = new List<float> {
                0f
            };
            animationStartTimes[i] = -1;
            timeTillAdd[i] = Random.Range(1f, 2.5f);
        }
    }

    // Update is called once per frame
    void Update() {
        addAnimations();

        for(int i = 0; i < armatures.Length; i++) {
            if(camTimeTargets[i].Count > 1) {
                updateCam(i);
            }
        }
    }

    private void addAnimations() {
        for(int i = 0; i < timeTillAdd.Length; i++) {
            timeTillAdd[i] -= Time.deltaTime;
            if(timeTillAdd[i] <= 0f) {
                addAnimation(i);
            }
        }
    }
    private void addAnimation(int cam) {
        float target = Random.Range(overshootAmmount, 1f-overshootAmmount);
        float overshoot = target + Mathf.Sign(target - camTimeTargets[cam][camTimeTargets[cam].Count-1]) * overshootAmmount;

        camTimeTargets[cam].Add(overshoot);
        camTimeTargets[cam].Add(target);

        timeTillAdd[cam] = Random.Range(0.5f, 1f);
    }

    private void updateCam(int cam) {
        //If a new animation is started
        if(animationStartTimes[cam] < 0) {
            animationStartTimes[cam] = Time.time;
        }
        
        //Getting relevant lerp/transform info is started
        float lastTarget = camTimeTargets[cam][0];
        float currTarget = camTimeTargets[cam][1];
        Transform lenseBone = armatures[cam].GetChild(0);
        Transform rotatorBone = armatures[cam].GetChild(1);
        float currAnimTime = Mathf.Clamp((Time.time - animationStartTimes[cam]) * speed, 0, 1);

        lenseBone.localPosition = Vector3.forward * Mathf.Lerp(minimumExtension, maximumExtension, Mathf.Lerp(lastTarget, currTarget, currAnimTime));
        rotatorBone.localEulerAngles = new Vector3(Mathf.Lerp(lastTarget, currTarget, currAnimTime), -1, -1) * 90f;
        
        //Go the the next Target if curret has completed
        if(currAnimTime == 1) {
            nextTarget(cam);
        }
    }

    private void nextTarget(int cam) {
        camTimeTargets[cam].RemoveAt(0);
        if(camTimeTargets[cam].Count > 1) {
            animationStartTimes[cam] = Time.time;
        } else {
            animationStartTimes[cam] = -1;
        }
    }
}
