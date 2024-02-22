using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;

public class LerpText : MonoBehaviour
{
    [SerializeField]
    private float amplitude = 50;

    [SerializeField]
    private float frequency = 1;

    [SerializeField]
    private Vector3 endPosModifiers;
    [SerializeField]
    private float duration;

    private Vector3 startPos;

    private Vector3 endPos;

    private Vector3 newPos;
    private float elapsedTime;

    private RectTransform rectTransform;

  
    void Start(){
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.position;
        endPos = new Vector3 (startPos.x + endPosModifiers.x, startPos.y + endPosModifiers.y, startPos.z + endPosModifiers.z);
        
    }
    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime/duration;
        
        newPos = Vector3.Lerp(startPos, endPos, percentageComplete);
        Debug.Log(percentageComplete);
        if (percentageComplete < 1){
            newPos.x += amplitude * Mathf.Sin(2 * Mathf.PI * frequency * elapsedTime);
        }
        
        rectTransform.position = newPos;
    }
}
