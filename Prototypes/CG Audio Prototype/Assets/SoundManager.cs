using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEngine;
using Scene = UnityEngine.SceneManagement.Scene;
using UnityEngine.Audio;
using System;
//This will process audio and play it, also will take into account settings
//Will add lasting choices
public class SoundManager : MonoBehaviour
{
    bool SFXACTIVE;
    bool MUSICACTIVE;
    [SerializeField] private AudioMixer audioMixer;
    
    void Start()
    {
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
