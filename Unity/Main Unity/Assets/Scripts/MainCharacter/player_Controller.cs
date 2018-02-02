using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class player_Controller : MonoBehaviour 
{
	public static CharacterController characterController; // getting character controller reference
	public static player_Controller Instance; // naming reference to this script in a reference to itself.
	public FuelLevel fuelSlider;
    public Rigidbody thePlayer;
    public bool playerDead = false;

    //references to enemies
    public static EnemyDeath enemyDeath; // to control when the enemy dies/is stunned
	public Chase chaseScript; //refence to chase script for when player has thrown grenade

	//Light object
	public Light lantern;



	//burn object
	public GameObject burnObj;
	bool canBurnObj = false;
    ParticleSystem burnparticle;

    //light object
    bool canLightObj = false;
    GameObject lightObj;


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
		fuelSlider = GetComponent<FuelLevel>();
		lantern = lantern.GetComponent<Light> ();
		lerpValue = 0f;

	}


    // Update is called once per frame
    void Update()
    {
        if (Camera.main == null)
        {
            return; // if there is no camera, do nothing.
        }

        if (playerDead == false)
        {
            GetMovementInput();
            HandleActionInput();
            player_Motor2.Instance.UpdateMotor();
        }




		if (playerDead) {
			PlayerDeath ();
		}
        if (resetColor) //resetting the colour of the lantern
        {
            lerpValue += Time.deltaTime / lerpDuration;
            lantern.color = Color.Lerp(lantern.color, Color.white, lerpValue);
            if (lantern.color == Color.white)
            {
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
            if (Input.GetAxis("Vertical") > deadzone || Input.GetAxis("Vertical") < -deadzone) // get vertical axis || if less than or further away than deadzone

            {
                player_Motor2.Instance.moveVector += new Vector3(0, 0, Input.GetAxis("Vertical"));
            }


            if (Input.GetAxis("Horizontal") > deadzone || Input.GetAxis("Horizontal") < -deadzone) // get horizontal axis || if less than or further away than deadzone

            {
                player_Motor2.Instance.moveVector += new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            }
            player_AnimatorController.Instance.CurrentDirection();

        

    }

	void HandleActionInput()
	{
        //check if youve pushed fire

            if (Input.GetKeyUp(KeyCode.E)) //When fire is released
            {

                if (canBurnObj)
                {
                    player_AnimatorController.Instance.PlayerBurn();
                    lantern.color = Color.red;
                    lerpCol = lantern.color;

                    BurnObject();
                }

                if (canLightObj)
                {
                    LightObject();
                }


            }
        

	}

	public void BurnObject()
	{
		if (fuelSlider.fuelSlider.value >= 0 && canBurnObj == true)
		{
            
            burnObj.transform.GetChild(0).gameObject.SetActive(true);
            fuelSlider.fuelDecrease = 3;
            fuelSlider.RemoveFuel();

            if (burnObj.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().isEmitting == false)
            {

            }
            burnObj.SetActive(false);
            canBurnObj = false;

        }


		if (fuelSlider.fuelSlider.value <= 0) 
		{
			Debug.Log ("Not enough Light to push objects.");
			return;
		}
		StartColourReset();
	}

    public void LightObject()
    {
        if (fuelSlider.fuelSlider.value >= 0 && canLightObj == true)
        {
            lightObj.transform.GetChild(0).gameObject.SetActive(true);
            fuelSlider.fuelDecrease = 5;
            fuelSlider.RemoveFuel();
            canLightObj = false;
        }


        if (fuelSlider.fuelSlider.value <= 0)
        {
            Debug.Log("Not enough Light to push objects.");
            return;
        }
    }

    

    public void PlayerDeath()
	{     
            //playerDead = true;

            
            //player_Motor2.Instance.moveVector = new Vector3(0, 0, 0);
            player_AnimatorController.Instance.PlayerDie();
		Debug.Log("player dead");
		playerDead = false;
        thePlayer.transform.position = CheckPoint.GetActiveCheckPointPosition();

    }


	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.tag == "burnable") 
		{
           
            burnObj = other.gameObject;
            
			canBurnObj = true;
		}

        if (other.gameObject.tag == "playerLight")
        {
            canLightObj = true;
            lightObj = other.gameObject;
        }

	}

	void OnTriggerExit(Collider other)
	{


		if (other.gameObject.tag == "burnable") 
		{
			burnObj = null;
			canBurnObj = false;
			
		}

        if (other.gameObject.tag == "playerLight")
        {
            lightObj = null;
            canLightObj = false;
        }

    }

	}



