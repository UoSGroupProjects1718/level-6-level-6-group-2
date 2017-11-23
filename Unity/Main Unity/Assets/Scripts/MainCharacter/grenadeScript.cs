using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeScript : MonoBehaviour {

	public Rigidbody grenade;
	public float grenadeForce;
	bool hasThrown = true;

	public GameObject DirectionFinder;
	// Use this for initialization

	public float waitTime = 3;

	//to set canThrowGrenade true when this object deletes itself.
	public GameObject playerObj;

	//set player variable on Chase to be null
	public GameObject chaseEnemy;

	//to find enemys

	void Start ()
	{
		DirectionFinder = GameObject.Find ("grenade_target");
		playerObj = GameObject.FindGameObjectWithTag ("Player");
		chaseEnemy = GameObject.Find ("enemy_chase");

		//grenade = GetComponent<Rigidbody>();
		//hasThrown = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (hasThrown) 
		{
			AddForceToGrenade ();
		}
		if (hasThrown == false) 
		{
			waitTime = waitTime -= Time.deltaTime;
			if (waitTime <= 0) 
			{
				DestroyObject ();
			}
		}
		//add rebound to object when it hits a collider
		
	}
	void AddForceToGrenade()
	{
		//addforce to object to throw it
		//grenade.AddForce(DirectionFinder.transform.forward * grenadeForce);
		grenade.AddForce(DirectionFinder.transform.forward * grenadeForce);

		hasThrown = false;
	}
	void DestroyObject()
	{		
		playerObj.GetComponent<player_Controller> ().canThrowGrenade = true; // set players CanThrowGrenade bool to true.
		//chaseEnemy.GetComponent<Chase>().player = null; //set the chase enemy player varibale to null so there is no grenade to chase
		Destroy (this.gameObject);
	}






}
