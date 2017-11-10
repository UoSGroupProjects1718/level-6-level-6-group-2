using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class player_Controller : MonoBehaviour 
{
	public static CharacterController characterController; // getting character controller reference
	public static player_Controller Instance; // naming reference to this script in a reference to itself.
	public static FuelLevel FuelLevel;
	public static EnemyDeath enemyDeath;

	//grenade object
	public GameObject grenade;
	public Transform grenadeTarget;


	//list of enemys can attack

	List<GameObject> enemyObjs = new List<GameObject>();


	// Use this for initialization
	void Awake () 
	{
		characterController = GetComponent("CharacterController") as CharacterController; // because it returns of tyoe object need to convert its type (as CharacterController)
		Instance = this; // points to the isntance created by unity of this script.
		FuelLevel = GetComponent<FuelLevel>();


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

		//if (Input.GetKeyUp (KeyCode.Mouse1)) 
		//{
			//Attack (transform.position, 5);
		//}
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
		//input from player?
		if (Input.GetButton ("Jump")) 
		{
			Jump ();
		}
		if (Input.GetKeyUp (KeyCode.Mouse0)) 
		{
			player_AnimatorController.Instance.PlayerAttack ();
			Attack ();


		}
	}

	void Jump() // anything i need jump to do should go here
	{
		player_Motor2.Instance.Jump ();
		//animation
		//sound effect
	}

	public void Attack()
	{	

		if (FuelLevel.fuelSlider.value >= 0)
		{
			//instanciate object
			GameObject tempGrenadeObject = Instantiate(grenade, grenadeTarget.transform.position, Quaternion.identity);
			//Instantiate(grenade, new Vector3(0,0,0), Quaternion.identity);
			
			FuelLevel.fuelSlider.value -= 5;
			Debug.Log (FuelLevel.fuelSlider.value);
		}

		
	}

	public void PlayerDeath()
	{
		Debug.Log ("player dead");
		player_Motor2.Instance.moveVector = new Vector3 (0, 0, 0);
		player_AnimatorController.Instance.PlayerDie ();
	}





	}



