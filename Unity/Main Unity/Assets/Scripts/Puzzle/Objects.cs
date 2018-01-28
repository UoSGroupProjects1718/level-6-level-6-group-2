using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour {
    public GameObject exitNode;
    public bool exitReached = false;
    public GameObject destroyed; 
    public Transform blockade;
   
    //to turn the lights on when completed puzzle
	public Transform lightOne;
	public Transform lightTwo;
    public GameObject light1;
	public GameObject light2;


 
    public void hitTarget(bool didHit)
    {
        if (didHit)
        {
			//get the child object of the brazier
			lightOne = light1.gameObject.transform.GetChild (0);
			//enable it to turn the fire on
			lightOne.gameObject.SetActive (true);
			lightTwo = light2.gameObject.transform.GetChild (0);
			lightTwo.gameObject.SetActive (true);

            exitReached = true;
            StartCoroutine(Block());
        }
    }
    IEnumerator Block()
    {
        Instantiate(destroyed, blockade.position, blockade.rotation);
        Destroy(exitNode);
        destroyed.gameObject.SetActive(false);
        yield return null;
    }

}
