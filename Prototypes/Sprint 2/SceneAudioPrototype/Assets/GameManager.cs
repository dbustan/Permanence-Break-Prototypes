using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using JetBrains.Annotations;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public int currentSaveNum;



    private void Start(){
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded (UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode){
        if (scene.name == "MainMenu"){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    
 
    


  
    

  
}
