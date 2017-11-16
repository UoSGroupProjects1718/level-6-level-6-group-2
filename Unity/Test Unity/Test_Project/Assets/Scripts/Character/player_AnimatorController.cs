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



	
	}

	public void PlayerDie()
	{

		anim.SetTrigger("isDying");
	}

	public void PlayerAttack()
	{

		anim.SetTrigger ("isAttacking");

	}
	public void PlayerBurn()
	{
		anim.SetTrigger ("isBurning");
	}
	public void PlayerPush()
	{
		anim.SetTrigger ("isPushing");
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

			anim.SetBool ("isIdle", false);
			anim.SetFloat ("horizontal", 1);
			forward = true;



		}
		if (player_Motor2.Instance.moveVector.z < 0) 
		{
			anim.SetBool ("isIdle", false);
			anim.SetFloat ("horizontal", -1);
			backward = true;

		}
		if (player_Motor2.Instance.moveVector.x > 0) 
		{
			anim.SetBool ("isIdle", false);
			anim.SetFloat ("vertical", -1);
			right = true;
		}
		if (player_Motor2.Instance.moveVector.x < 0) 
		{
			anim.SetBool ("isIdle", false);
			anim.SetFloat ("vertical", 1);
			left = true;
		}

		if (forward) {

			if (left) {
				
				MoveDirection = Direction.LeftForward;
			} else if (right) {
				MoveDirection = Direction.RightForward;
			} else {
				MoveDirection = Direction.Forward;
			}

		} 
		else if (backward) {


			if (left) {
				
				MoveDirection = Direction.LeftBackward;
			} else if (right) {
				MoveDirection = Direction.RightBackward;
			} else {
				MoveDirection = Direction.Backward;	
			}

		} 
		else if (left) {


			MoveDirection = Direction.Left;


		} 
		else if (right) {


			MoveDirection = Direction.Right;

		}

		else 
		{

			MoveDirection = Direction.Stationary;
			anim.SetFloat ("vertical", 0);
			anim.SetFloat ("horizontal", 0);
			anim.SetBool ("isIdle", true);


		}

	}

}
