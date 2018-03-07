using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {

    public bool correctPlate;
    public GameObject pressureController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && correctPlate == false)
        {
            pressureController.GetComponent<PressurePlateController>().WrongPlate();
        }

        if (other.gameObject.tag == "Player" && correctPlate)
        {
            pressureController.GetComponent<PressurePlateController>().CorrectPlate();
        }

    }
}
