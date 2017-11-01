using UnityEngine;
using System.Collections;

public class player_Controller : MonoBehaviour 
{
	public static CharacterController characterController; // getting character controller reference
	public static player_Controller Instance; // naming reference to this script in a reference to itself.


	// Use this for initialization
	void Awake () 
	{
		characterController = GetComponent("CharacterController") as CharacterController; // because it returns of tyoe object need to convert its type (as CharacterController)
		Instance = this; // points to the isntance created by unity of this script.


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
	}

	void Jump() // anything i need jump to do should go here
	{
		player_Motor2.Instance.Jump ();
		//animation
		//sound effect
	}

	/*public void Attack(Vector3 centre, float radius)
	{
		Collider[] hitColliders = Physics.OverlapSphere (centre, radius);
		int i = 0;
		while (i < hitColliders.Length) 
		{
			Debug.Log(hitColliders [i]);
			//Damage enemy here
			i++;
		}
		
	}*/


}
