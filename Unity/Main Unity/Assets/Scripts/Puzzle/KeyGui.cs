using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyGui : MonoBehaviour {

    public Text PressE;

    void Awake()
    {

        PressE.enabled = false;

    }

    void Update()
    {

    }


    void OnTriggerEnter(Collider KeyGUI)
    {
        if (KeyGUI.gameObject.tag == "Player")
        {
            PressE.enabled = true;
        }
    }

    void OnTriggerExit(Collider KeyGUI)
    {
        if(KeyGUI.gameObject.tag == "Player")
        {
            PressE.enabled = false;
        }
    }


}
