using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeScript : MonoBehaviour {

	public Rigidbody grenade;
	public float grenadeForce;
	bool hasThrown = true;
	public List<GameObject> enemies = new List<GameObject>();
	public GameObject DirectionFinder;
	// Use this for initialization

	public float waitTime = 3;

	//to find enemys

	void Start ()
	{
		DirectionFinder = GameObject.Find ("grenade_target");

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
	void DestroyObject()
	{		
		Destroy (this.gameObject);
	}

	void AddForceToGrenade()
	{
		//addforce to object to throw it
		grenade.AddForce(DirectionFinder.transform.forward * grenadeForce);
		hasThrown = false;
	}



	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy") 
		{
			//once within trigger wait for 3 seconds
			//then return enemies to patrol

		}
	}


}
