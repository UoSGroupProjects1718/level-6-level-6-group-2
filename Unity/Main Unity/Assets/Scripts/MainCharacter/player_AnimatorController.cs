using UnityEngine;
using System.Collections;

public class player_AnimatorController : MonoBehaviour 
{
	

	public enum Direction
	{
 		Stationary, Forward, Backward, Left, Right, LeftForward, RightForward, LeftBackward, RightBackward
	}
	public static player_AnimatorController Instance;
	public Direction MoveDirection { get; set;}
	public Animator anim;

	void Awake () 
	{
		Instance = this;
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	

			

		if (Input.GetKeyUp (KeyCode.M)) 
		{
			PlayerReceiveAttack ();
		}
	}

	public void PlayerDie()
	{

		anim.SetTrigger("isDying");
	}

	public void PlayerAttack()
	{
		
		anim.SetTrigger ("isAttacking");

	}
	public void PlayerReceiveAttack()
	{
		anim.SetTrigger ("isTakeDamage");
	}

	public void CurrentDirection()
	{
		var forward = false;
		var backward = false;
		var left = false;
		var right = false;

		if (player_Motor2.Instance.moveVector.z > 0) 
		{
			
			forward = true;

		}
		if (player_Motor2.Instance.moveVector.z < 0) 
		{
			
			backward = true;
		}
		if (player_Motor2.Instance.moveVector.x > 0) 
		{
			
			right = true;
		}
		if (player_Motor2.Instance.moveVector.x < 0) 
		{
			
			left = true;
		}

		if (forward) {
			anim.SetFloat ("horizontal", 1);

			if (left)
				MoveDirection = Direction.LeftForward;
			else if (right)
				MoveDirection = Direction.RightForward;
			else
				MoveDirection = Direction.Forward;
			
		} else if (backward) {
			
			anim.SetFloat ("horizontal", -1);
			if (left)
				MoveDirection = Direction.LeftBackward;
			else if (right)
				MoveDirection = Direction.RightBackward;
			else
				MoveDirection = Direction.Backward;	
			
		} 
		else if (left) {
			
			anim.SetFloat ("vertical", 1);
			MoveDirection = Direction.Left;

		} 
		else if (right) {
			
			anim.SetFloat ("vertical", -1);
			MoveDirection = Direction.Right;

		}
		
		else 
		{
			MoveDirection = Direction.Stationary;
			anim.SetFloat ("horizontal",0);

		}

	}
}
