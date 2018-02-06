using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyGui : MonoBehaviour {

    public GameObject PressE;

    void Awake()
    {

        // PressE.enabled = false;
        PressE.SetActive(false);

    }

    void Update()
    {

    }


    void OnTriggerEnter(Collider KeyGUI)
    {
        if (KeyGUI.gameObject.tag == "Player")
        {
            
            //PressE.enabled = true;
            PressE.SetActive(true);
        }
    }

    void OnTriggerExit(Collider KeyGUI)
    {
        if(KeyGUI.gameObject.tag == "Player")
        {
            //PressE.enabled = false;
            PressE.SetActive(false);
        }
    }


}
