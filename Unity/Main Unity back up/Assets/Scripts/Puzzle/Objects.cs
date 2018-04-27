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


    //door object
    public GameObject gate;
	public GameObject player;


    public AudioSource puzzleComplete;
    public AudioClip machinery;
    public AudioClip puzzleCompleteAudio;

    void Start()
	{
        puzzleComplete = GetComponent<AudioSource>();
    
		player = GameObject.FindGameObjectWithTag("Player");
	}

    public void hitTarget(bool didHit)
    {
        if (didHit)
        {
            puzzleComplete.Stop();
            puzzleComplete.clip = puzzleCompleteAudio;
            puzzleComplete.Play();
            //get the child object of the brazier
           // light1.gameObject.transform.GetChild (0).gameObject.SetActive(true);
            lightOne.gameObject.transform.GetChild(0).gameObject.SetActive(true);
			//enable it to turn the fire on
			lightOne.gameObject.SetActive (true);
			lightTwo.gameObject.transform.GetChild (0).gameObject.SetActive(true);
			lightTwo.gameObject.SetActive (true);


            exitReached = true;


            
            StartCoroutine(Block());
        }
    }
    IEnumerator Block()
    {
        // Instantiate(destroyed, blockade.position, blockade.rotation);
        //Destroy(exitNode);
        destroyed.gameObject.SetActive(false);
        gate.gameObject.SetActive(false);



        yield return null;
    }

}
