using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour {
    public Transform player;
    static Animator anim;


	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {

		//if there is a Transform in the player variable
		if (player != null) 
		{
			GoToGrenade (); //send enemy to grenade
		}


	

	}

	public void GoToGrenade() // put this in its own function so it can be called when a grenade is thrown
	{
		Vector3 direction = player.position - this.transform.position;
		float angle = Vector3.Angle(direction, this.transform.forward);
		if (Vector3.Distance(player.position, this.transform.position) < 8 && angle < 50)
		{        
			direction.y = 0;

			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, 
				Quaternion.LookRotation(direction), 2f * Time.deltaTime);

			anim.SetBool("isIdle", false);

			if(direction.magnitude > 1.5f)
			{
				this.transform.Translate(0, 0, 0.05f);
				anim.SetBool("isWalking", true);
				anim.SetBool("isAttacking", false);
			}
			else
			{
				anim.SetBool("isAttacking", true);
				anim.SetBool("isWalking", false);
			}
		}
		else
		{
			anim.SetBool("isIdle", true);
			anim.SetBool("isWalking", false);
			anim.SetBool("isAttacking", false);
		}

	}


}
