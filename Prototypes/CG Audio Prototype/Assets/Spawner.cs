using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject cubePrefab;
   
    void Start()
    {
        
    }

    
    
    public void RandomPos()
    {
        float randPosX = Random.Range(-24.0f, 24.0f);
        float randPosY = Random.Range(3.0f, 10.0f);
        float randPosZ = Random.Range(-24.0f, 24.0f);
        Vector3 pos = new Vector3(randPosX, randPosY, randPosZ);
        transform.position = pos;
    }
}
