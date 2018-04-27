using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGateScript : MonoBehaviour {

    public GameObject[] gates;
    public bool gateOpen;



	// Use this for initialization
	void Start () {

        gateOpen = false;
	}
	
	// Update is called once per frame
	void Update () {



        if (gateOpen)
        {
            for (int i = 0; i < gates.Length; i++)
            {
                gates[i].SetActive(false);
            }
        }


		
	}

    
}
