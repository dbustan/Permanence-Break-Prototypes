using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private GameManager gm; 
    private void Start(){
        gm = FindAnyObjectByType<GameManager>();
        if (gm == null){
            Debug.Log("Cannot find Game manager!");
        }
    }
    public void SaveGame(){
        SaveSystem.Save(gm.currentSaveNum, "Level 0");
    }
    public void LoadOptions(){
        //Nope not yet
    }

    public void ReturnToMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}
