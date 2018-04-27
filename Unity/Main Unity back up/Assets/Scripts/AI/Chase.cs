using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour {
    public Transform player;
    static Animator anim;
	public Vector3 direction;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        direction = player.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);
        if (Vector3.Distance(player.position, this.transform.position) < 8 && angle < 50)
        {        
			ChasePlayer ();
        }
        else
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);
        }
		
	}

	public void ChasePlayer()
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

	public void EnemyDie()
	{
		Debug.Log ("enemy die for x seconds");
	}
}
