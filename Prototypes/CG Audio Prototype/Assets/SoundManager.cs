using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEngine;
using Scene = UnityEngine.SceneManagement.Scene;
using UnityEngine.Audio;
using System;
using System.Runtime.CompilerServices;
using System.Linq;
//This will process audio and play it, also will take into account settings
//Will add lasting choices

//Character needs 
public class SoundManager : MonoBehaviour
{
    private List<GameObject> FootstepSounds; 
    bool footAlternate;
    bool SFXACTIVE;
    bool MUSICACTIVE;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject walkingPrefab;
    private Character playerScript;
    

    void Start()
    {
        playerScript = player.GetComponent<Character>();
        SFXACTIVE = true;
        MUSICACTIVE = true;
        footAlternate = false;

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerWalk()
    {
        GameObject newSource;
        //Left side
       if (!footAlternate)
        {
            Vector3 placementOffset = new Vector3(0, 1, 0.3f);
            newSource = Instantiate(walkingPrefab, player.transform.position - placementOffset, Quaternion.identity);
        }
       //Right side
        else
        {
            Vector3 placementOffset = new Vector3(0, 1, -0.3f);
            newSource = Instantiate(walkingPrefab, player.transform.position - placementOffset, Quaternion.identity);
        }
        footAlternate = !footAlternate;
        AudioSource footstepAudio = newSource.GetComponent<AudioSource>();
        footstepAudio.Play();
        
       



    }

  
    

    

}
