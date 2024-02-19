using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, ButtonTrigger
{
    public bool defaultOpen;

    void Start() {
        gameObject.SetActive(!defaultOpen);
    }

    public void buttonPress()
    {
        gameObject.SetActive(defaultOpen);
    }

    public void buttonRelease()
    {
        gameObject.SetActive(!defaultOpen);
    }
}
