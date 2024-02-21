using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] Button Play, Options, Quit;
    [SerializeField] GameObject loadingScreen, configScreen, mainMenuCanvas, mainMenuScreen, optionsScreen, saveSlotScreen;
    
    GameObject currentScreen;
    List<GameObject> Dots = new List<GameObject>();
    int curr = 0;

    private void Start() {
        currentScreen = loadingScreen;
        GrabLoadingTextObj(loadingScreen.transform);
        Invoke("SwitchToConfig", 5);
    }

    private void GrabLoadingTextObj(Transform loadingScreenTransform) {
        foreach (Transform child in loadingScreenTransform) {
            if (child.tag == "Loading") LoadingEllipsis(child.transform);
        }
    }

    private void LoadingEllipsis(Transform loadingTextObj) {
        int count = 1;
        foreach (Transform child in loadingTextObj) {
            Dots.Add(child.gameObject);
            Invoke("setDotActive", count);
            count++;
        }
    }

    private void setDotActive() {
        Dots[curr].SetActive(true);
        curr++;
    }

    private void SwitchToConfig() {
        currentScreen.SetActive(false);
        mainMenuCanvas.SetActive(true);
        currentScreen = configScreen;
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
