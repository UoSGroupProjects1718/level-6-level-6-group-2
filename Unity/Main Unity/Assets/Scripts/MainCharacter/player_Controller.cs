using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class player_Controller : MonoBehaviour 
{
	public static CharacterController characterController; // getting character controller reference
	public static player_Controller Instance; // naming reference to this script in a reference to itself.
	public static FuelLevel FuelLevel;
    private Rigidbody thePlayer;

	//references to enemies
	public static EnemyDeath enemyDeath; // to control when the enemy dies/is stunned
	public Chase chaseScript; //refence to chase script for when player has thrown grenade

	//Light object
	public Light lantern;

	//grenade object
	public GameObject grenade;
	public Transform grenadeTarget;
	public bool canThrowGrenade = false;


	//push object
	public GameObject pushObj;
	bool canPushObj = false;
	public float pushForce = 10f;

	//burn object
	public GameObject burnObj;
	bool canBurnObj = false;


	//return light back to white
	private Color lerpCol;
	private bool resetColor;
	private float lerpValue;
	private float lerpDuration = 10f;

	//bool to check if player is near a mirror
	public bool isNearMirror;

	// Use this for initialization
	void Awake () 
	{
		characterController = GetComponent("CharacterController") as CharacterController; // because it returns of tyoe object need to convert its type (as CharacterController)
		Instance = this; // points to the isntance created by unity of this script.
		FuelLevel = GetComponent<FuelLevel>();
		lantern = lantern.GetComponent<Light> ();

		lerpValue = 0f;
	}

	
	// Update is called once per frame
	void Update () 
	{
		if (Camera.main == null) 
		{
			return; // if there is no camera, do nothing.
		}

		GetMovementInput ();
		HandleActionInput ();
		player_Motor2.Instance.UpdateMotor();


		if (resetColor) //resetting the colour of the lantern
		{
			lerpValue += Time.deltaTime / lerpDuration;
			lantern.color = Color.Lerp (lantern.color, Color.white, lerpValue);
			if (lantern.color == Color.white) {
				lerpValue = 0f;
				resetColor = false;
			}
		}
	}

	void StartColourReset()
	{
		lerpValue = 0f;
		resetColor = true;
	}

	void GetMovementInput() // getting info from playerMotor about movement
	{
		//establish a deadzone
		var deadzone = 0.1f; // f assigns as a float
		//gravity
		player_Motor2.Instance.verticalVelocity = player_Motor2.Instance.moveVector.y;
		player_Motor2.Instance.moveVector = Vector3.zero; // zero out moveVector, stops motion being additive as we are renewing info each frame
		if(Input.GetAxis("Vertical") > deadzone || Input.GetAxis("Vertical") < -deadzone) // get vertical axis || if less than or further away than deadzone

		{
			player_Motor2.Instance.moveVector += new Vector3(0,0,Input.GetAxis("Vertical"));
		}


		if(Input.GetAxis("Horizontal") > deadzone || Input.GetAxis("Horizontal") < -deadzone) // get horizontal axis || if less than or further away than deadzone
			
		{
			player_Motor2.Instance.moveVector += new Vector3(Input.GetAxis("Horizontal"),0,0);
		}
		player_AnimatorController.Instance.CurrentDirection ();

	}

	void HandleActionInput()
	{


			//check if youve pushed fire

			if (Input.GetKeyDown (KeyCode.Mouse0)) //if hold fire down
			{
				if(canPushObj) //push object
				{
					player_AnimatorController.Instance.PlayerPush ();
					lantern.color = Color.blue;
					lerpCol = lantern.color;
					PushObject ();
				}
			}
			//
			if (Input.GetKeyUp (KeyCode.Mouse0)) //When fire is released
			{

				if(canThrowGrenade)
				{
				player_AnimatorController.Instance.PlayerAttack ();	
				lantern.color = Color.green;
					lerpCol = lantern.color;
					
					//determine which attack should be used
					Attack ();
				}

				if(canBurnObj)
				{
				player_AnimatorController.Instance.PlayerBurn ();	
				lantern.color = Color.red;
					lerpCol = lantern.color;
					
					BurnObject ();
				}

				if (pushObj.transform.parent = this.gameObject.transform) 
				{
					pushObj.transform.parent = null;
				}

			}
		



	}




	public void Attack()
	{	

		if (FuelLevel.fuelSlider.value >= 0 && canThrowGrenade == true)
		{
			//instanciate object
			GameObject tempGrenadeObject = Instantiate(grenade, grenadeTarget.transform.position,Quaternion.identity);
			//Instantiate(grenade, new Vector3(0,0,0), Quaternion.identity);
			FuelLevel.fuelSlider.value -= 5;
			canThrowGrenade = false;

			//send enemy to grenade
			chaseScript.player = tempGrenadeObject.transform;
			//Debug.Log (chaseScript.player.name);

			//Debug.Log (FuelLevel.fuelSlider.value);

		}


		if (FuelLevel.fuelSlider.value <= 0) 
		{
			Debug.Log ("Not enough Light to throw grenades.");
			return;
		}

		StartColourReset();
	}
	public void PushObject()
	{


		if (FuelLevel.fuelSlider.value >= 0 && canPushObj == true)
		{
			//pushObj.GetComponent<Rigidbody> ().AddForce (transform.forward * pushForce);
			pushObj.transform.parent = this.gameObject.transform;
			FuelLevel.fuelSlider.value -= 5;
			canPushObj = false;

		}


		if (FuelLevel.fuelSlider.value <= 0) 
		{
			Debug.Log ("Not enough Light to push objects.");
			return;
		}

		StartColourReset();
	}
	public void BurnObject()
	{


		if (FuelLevel.fuelSlider.value >= 0 && canBurnObj == true)
		{
			burnObj.SetActive (false);
			FuelLevel.fuelSlider.value -= 5;
			canBurnObj = false;
			canThrowGrenade = true;

		}


		if (FuelLevel.fuelSlider.value <= 0) 
		{
			Debug.Log ("Not enough Light to push objects.");
			return;
		}
		StartColourReset();
	}

	public void PlayerDeath()
	{
		Debug.Log ("player dead");
		player_Motor2.Instance.moveVector = new Vector3 (0, 0, 0);
		player_AnimatorController.Instance.PlayerDie ();
        thePlayer.transform.position = CheckPoint.GetActiveCheckPointPosition();
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "pushable") 
		{
			canThrowGrenade = false;
			pushObj = other.gameObject;

			canPushObj = true;
		}
		if (other.gameObject.tag == "burnable") 
		{
			canThrowGrenade = false;
			burnObj = other.gameObject;

			canBurnObj = true;
		}
		if (other.gameObject.tag == "Reflect") 
		{
			canThrowGrenade = false;
			isNearMirror = true;

		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "pushable") 
		{			
			pushObj = null;
			canPushObj = false;
			canThrowGrenade = true;
		}

		if (other.gameObject.tag == "burnable") 
		{
			burnObj = null;
			canBurnObj = false;
			canThrowGrenade = true;
		}
		if (other.gameObject.tag == "Reflect") 
		{
			isNearMirror = false;
			canThrowGrenade = true;
		}
	}

	}



