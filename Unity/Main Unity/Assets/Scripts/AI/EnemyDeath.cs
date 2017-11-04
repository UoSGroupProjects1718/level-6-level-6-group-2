using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour {

	FuelLevel fuelLevel;

	Animator anim;
	int enemyHealth = 2;

	float deathTimer = 10;
	bool enemyDead = false;

	public float enemyDamage = 20;
	Transform target;

	bool enemyAttacking = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		fuelLevel = GameObject.FindGameObjectWithTag ("Player").GetComponent<FuelLevel> ();
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {

		if (enemyDead) 
		{
			deathTimer -= Time.deltaTime;
			if (deathTimer <= 0) 
			{
				anim.SetBool ("isDead", false);
				enemyDead = false;
			}
		}

		if (enemyAttacking) 
		{
			AttackPlayer ();
		}


	}
	public void AttackPlayer()
	{
		fuelLevel.fuelSlider.value -= enemyDamage;

		transform.LookAt (target);
		anim.SetTrigger ("isAttacking");
		enemyAttacking = false;
	}

	public void ReceiveHit()
	{
		if (enemyHealth >= 0) 
		{
			enemyHealth -= 1;
			anim.SetTrigger ("isHit");
			Debug.Log (enemyHealth);
		}

		if (enemyHealth <= 0) 
		{
			enemyHealth = 0;
			EnemyDead ();

		}
	}
	public void EnemyDead()
	{
		anim.SetBool ("isDead", true);
		enemyDead = true;
		Debug.Log ("enemy dead");
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			
			enemyAttacking = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			
			enemyAttacking = false;
		}
	}


}
