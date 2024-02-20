using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VisibilityManager : MonoBehaviour
{
    public static bool passShouldRun = false;

    public ComputeShader objectIdsCheckShader, clearShader;
    public int maxObjectsInScene;
    private int nextId, numObjects;

    public RenderTexture pixelsTexture;

    public RawImage debugImage, snapshotImage;
    private RenderTexture debugTexture;

    // public Material visibleTestMat, hiddenTestMat;

    private Dictionary<int, GameObject> idToGameObject;
    private bool shaderSetup;
    
    void Start()
    {
        idToGameObject = new Dictionary<int, GameObject>();
        numObjects = 0;
        nextId = 1;
        
        getAllVisibilityObjects();
        debugTexture = new RenderTexture(pixelsTexture);
        debugImage.texture = debugTexture;

        passShouldRun = true;
    }

    private void Update() {
        if(pixelsTexture != null) {
            if(shaderSetup) resetVisibleObjects();
            checkObjectVisibility(pixelsTexture, debugTexture);
        }
    }

    private void OnDestroy() {
        DisposeMarchingCubesShader();
    }

    public void checkObjectVisibility(RenderTexture texture, RenderTexture debugTexture) {
        texture.enableRandomWrite = true;

        int pixelsWidth = texture.width;
        int pixelsHeight = texture.height;

        if(!shaderSetup)
            SetupObjectCheckShader(pixelsWidth, pixelsHeight);

        int kernel = objectIdsCheckShader.FindKernel("CheckObjectIds");

        objectIdsCheckShader.SetBuffer(kernel, "objectIds", objectIdsBuffer);

        objectIdsCheckShader.SetFloat("debugScale", numObjects);
        objectIdsCheckShader.SetTexture(kernel, "pixelsTexture", texture);
        objectIdsCheckShader.SetTexture(kernel, "debugTexture", debugTexture);
        Debug.Log("Test1");
        objectIdsCheckShader.Dispatch(kernel, Mathf.CeilToInt(pixelsWidth / 16f), Mathf.CeilToInt(pixelsHeight / 16f), 1);

        int[] objectVisibility = new int[maxObjectsInScene];
        objectIdsBuffer.GetData(objectVisibility);

        for(int i = 1; i <= numObjects; i++) {
            int objVisible = objectVisibility[i];
            VisibilityObject obj = idToGameObject[i].GetComponent<VisibilityObject>();
            if(!obj.grabbed) {
                if(objVisible == 0) {
                    obj.incrementUnseenFrames();
                } else {
                    obj.setVisible();
                }
            }
        }
    }

    private void getAllVisibilityObjects() {
        foreach(GameObject obj in FindObjectsOfType(typeof (GameObject))) {
            if(!obj.GetComponent<VisibilityObject>()) continue;
            Renderer renderer = obj.GetComponent<VisibilityObject>().mainRenderer;
            if(nextId > maxObjectsInScene) throw new System.Exception("Number of Permanence objects exceeds the maximum. Plese remove objects from the scene or increase the max.");

            MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();

            int objId = nextId;
            nextId++;
            numObjects++;
            obj.GetComponent<VisibilityObject>().setObjectId(objId);

            idToGameObject.Add(objId, obj);
            propertyBlock.SetFloat("_objectId", objId);
            renderer.SetPropertyBlock(propertyBlock);
        }
    }
    
    ComputeBuffer objectIdsBuffer;
    void SetupObjectCheckShader(int width, int height)
    {
        objectIdsBuffer = new ComputeBuffer(maxObjectsInScene, sizeof(int), ComputeBufferType.Structured);
        objectIdsCheckShader.SetInt("width", width);
        objectIdsCheckShader.SetInt("height", height);
        clearShader.SetInt("maxObjects", maxObjectsInScene);
        shaderSetup = true;
    }
    void DisposeMarchingCubesShader()
    {
        Debug.Log("Disposing of Compute Buffers");
        objectIdsBuffer.Dispose();
        shaderSetup = false;
    }
    void resetVisibleObjects() {
        int kernel = clearShader.FindKernel("ClearVisibleObjects");
        clearShader.SetBuffer(kernel, "objectIds", objectIdsBuffer);
        clearShader.Dispatch(kernel, Mathf.CeilToInt((maxObjectsInScene+1) / 16f), 1, 1);
    }
}
