using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTimerScript : MonoBehaviour {

	//this script is used to change the light colour/flicker the light over time.

	//Light object
	public Light lantern;


	//timer to change colour
	private float timerValue = 0;
	public float maxDist = 8.0f;
	public float speed = 4.0f;

	private bool lightOn;




	// Use this for initialization
	void Start () {
		lantern = lantern.GetComponent<Light> ();
		lightOn = true;
		lantern.intensity = 0;
		
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (lightOn) //if the light is on
		{
			lantern.intensity = Mathf.PingPong (timerValue * speed, maxDist);
			timerValue += Time.deltaTime;

				
		}
	



	}


}
