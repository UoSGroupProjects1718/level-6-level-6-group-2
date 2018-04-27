using UnityEngine;
using System.Collections;

public class player_Motor2 : MonoBehaviour 
{
	public static player_Motor2 Instance; // referencing this script
	public float forwardSpeed = 10f; // 10m/s
	public float backwardSpeed = 5f; // 10m/s
	public float sideSpeed = 7f; // 10m/s

	public float jumpSpeed = 6f; 

	public float gravity = 21f; //gravity value
	public float terminalVelocity = 20f; //gravity terminal velocity

	public Vector3 moveVector { get; set;}
	public float verticalVelocity{ get; set;} // gravity vertical velocity for jump
	// Use this for initialization
	void Awake() 
	{
		Instance = this;

    }
	
	// want to tell this when to update.
	public void UpdateMotor() 
	{
		ProcessMotion();
		SnapAlignCharacterWithCam ();
		
	}
	void ProcessMotion() // transform moveVector into world space, relative to player rotation
	{
		moveVector = transform.TransformDirection (moveVector);
		//normalize moveVector if magnitude > 1
		if (moveVector.magnitude > 1) 
		{
			moveVector = Vector3.Normalize(moveVector); // move vector = what is returned by Vector3, normalize by passing in the moveVector

			
		}
		
		
		//set a new magnitude for moveVector, based on moveSpeed. multiply moveVector by moveSpeed.
		moveVector *= MoveSpeed();

		//reapply vertical velocity to moveVector.y
		moveVector = new Vector3(moveVector.x, verticalVelocity, moveVector.z); 

		//apply gravity
		ApplyGravity();


		//now have to move player in world space based on the math calculations above. 
		player_Controller.characterController.Move(moveVector * Time.deltaTime);
	}

	void ApplyGravity()
	{
		if (moveVector.y > -terminalVelocity)
		{
			moveVector = new Vector3(moveVector.x, moveVector.y - gravity * Time.deltaTime, moveVector.z); // apply gravity

		}
		if (player_Controller.characterController.isGrounded && moveVector.y < -1) 
		{
			moveVector = new Vector3(moveVector.x, -1 - gravity * Time.deltaTime, moveVector.z); // apply gravity

		}
	}

	public void Jump()
	{
		//are we on the ground
		if (player_Controller.characterController.isGrounded) 
		{
			//apply vertical velocity
			verticalVelocity = jumpSpeed;
		}
	}

	void SnapAlignCharacterWithCam()
	{
		if(moveVector.x != 0 || moveVector.z != 0) // if moving in either direction we are moving
		{
			transform.rotation = Quaternion.Euler(transform.eulerAngles.x, //using players rotation
			                                      Camera.main.transform.eulerAngles.y, // using cameras rotation
			                                      transform.eulerAngles.z); //using players rotation
			
		}
		
	}

	float MoveSpeed()
	{
		var moveSpeed = 0f;

		switch (player_AnimatorController.Instance.MoveDirection) 
		{
			case player_AnimatorController.Direction.Stationary:
			moveSpeed = 0;
			break;

			case player_AnimatorController.Direction.Forward:
				moveSpeed = forwardSpeed;
				break;
			case player_AnimatorController.Direction.Backward:
				moveSpeed = backwardSpeed;
				break;
			case player_AnimatorController.Direction.Left:
				moveSpeed = sideSpeed;
				break;
			case player_AnimatorController.Direction.Right:
				moveSpeed = sideSpeed;
				break;
			case player_AnimatorController.Direction.RightBackward:
				moveSpeed = backwardSpeed;
				break;
			case player_AnimatorController.Direction.RightForward:
				moveSpeed = forwardSpeed;
				break;
			case player_AnimatorController.Direction.LeftBackward:
				moveSpeed = backwardSpeed;
				break;
			case player_AnimatorController.Direction.LeftForward:
				moveSpeed = forwardSpeed;
				break;
		}

		return moveSpeed;
	}
}
