using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] Button Play, Options, Quit;
    [SerializeField] GameObject mainMenuScreen, optionsScreen, saveSlotScreen;
    
    GameObject currentScreen;

    private void Start() {
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
    

}
