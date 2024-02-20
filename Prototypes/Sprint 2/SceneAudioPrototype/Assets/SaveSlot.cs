using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSlot : MonoBehaviour
{
    public int saveNum;
    public bool isEmpty;
    [SerializeField] public TextMeshProUGUI SaveInfoText;

    private void Start (){
  
    }

    public void StartNewSave(){
        SaveSystem.Save(saveNum, gameObject.name + " is not empty");
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm == null){
            Debug.Log("Error, unable to find Game manager");
        } 
    }
    private void OnEnable() {
        RefreshSaveSlot();
    }
    public void RefreshSaveSlot(){
        string SaveData = SaveSystem.LoadSave(saveNum);
        if (SaveData == null) {
            isEmpty = true;
            SaveInfoText.text = "IS EMPTY";
        } else {
            isEmpty = false;
            Debug.Log(gameObject.name);
            SaveInfoText.text = SaveData;
        }
    }

}
