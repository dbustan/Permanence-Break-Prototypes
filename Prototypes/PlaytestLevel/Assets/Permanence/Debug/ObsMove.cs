using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsMove : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");
        float yInput = Input.GetButton("Jump") ? 1 : (Input.GetButton("Fire3") ? -1 : 0);

        Vector3 movement = new Vector3(xInput, yInput, zInput);
        
        transform.position += movement * speed * Time.deltaTime;
    }
}
