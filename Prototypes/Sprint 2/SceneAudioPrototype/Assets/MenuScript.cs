using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] Button Play, Options, Quit;
    [SerializeField] GameObject UIParent, mainMenuScreen, optionsScreen, saveSlotScreen;

    [SerializeField] GameManager gm;
    GameObject currentScreen;

    bool trashingSave;
    private void Start() {
        trashingSave = false;
        currentScreen = mainMenuScreen;
    }

    public void OpenSaveScreen(){
        currentScreen.SetActive(false);
        saveSlotScreen.SetActive(true);
        currentScreen = saveSlotScreen;
    }

    public void OpenOptions(){
        currentScreen.SetActive(false);
        optionsScreen.SetActive(true);
        currentScreen = optionsScreen;
    }

    public void OpenMainMenu(){
        currentScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
        currentScreen = mainMenuScreen;
    }

    public void QuitGame(){
        Application.Quit();
    }

    private void StartGame(GameObject saveSlotObj){
        
    }

    public void HandleSaveClick(SaveSlot save){
        if (trashingSave) {
            SaveSystem.DeleteSave(save.saveNum);
            AudioSource[] audioSources = UIParent.GetComponents<AudioSource>();
            AudioSource deletionAudio = audioSources[3];
            deletionAudio.Play();
            save.SaveInfoText.text = "Empty";
        } else {
        gm.currentSaveNum = save.saveNum;
        SceneManager.LoadScene("Level1");
        }
    }

    public void ChangeToDeleting(){
        trashingSave = !trashingSave;
    }
    

}
