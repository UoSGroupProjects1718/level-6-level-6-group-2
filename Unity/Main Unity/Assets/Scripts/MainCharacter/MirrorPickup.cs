using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorPickup : MonoBehaviour {

    public GameObject pickUp;
    public GameObject temp;
    public Transform pickUpLocation;

	// Use this for initialization
	void Start () {
        pickUp.GetComponent<Rigidbody>().useGravity = false;
	}
	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetKeyDown(KeyCode.E))
        {
            pickUp.GetComponent<Rigidbody>().useGravity = false;
            pickUp.GetComponent<Rigidbody>().isKinematic = true;
            pickUp.transform.position = pickUpLocation.transform.position;
            pickUp.transform.rotation = pickUpLocation.transform.rotation;
            pickUp.transform.parent = temp.transform;
        }
        if(Input.GetKeyUp(KeyCode.E))
        {
            pickUp.GetComponent<Rigidbody>().useGravity = true;
            pickUp.GetComponent<Rigidbody>().isKinematic = false;
            pickUp.transform.parent = null;
            pickUp.transform.position = temp.transform.position;
        }      
	}
}
