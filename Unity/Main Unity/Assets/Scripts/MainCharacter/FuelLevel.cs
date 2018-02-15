using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FuelLevel : MonoBehaviour {

   
    public static CheckPoint checkpoint;

    public Slider fuelSlider;
    public int maxFuel;
    public int fuelFallRate;
    public Light Lantern;

    //decrease fuel 
    public int fuelDecrease;

    


    public bool playerDead = false;


    //Light object
    public Light lantern;



    //burn object
    public GameObject burnObj;
    bool canBurnObj = false;
    ParticleSystem burnparticle;

    //light object
    bool canLightObj = false;
    GameObject lightObj;



    //level switch once finished area
    public int currentLevel;

    //collecting feathers to open gate to get to puzzle
    public GateManager gateManager;

    // Use this for initialization
    void Start () {
        lantern = lantern.GetComponent<Light>();
         gateManager.GetComponent<GateManager>();
       
        lantern.intensity = 3f;
	}

	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp(KeyCode.E)) //When fire is released
        {

            if (canBurnObj)
            {

                BurnObject();
            }

            if (canLightObj)
            {
                LightObject();
            }


        }

        if (fuelSlider.value >= 0)
        {
           // fuelSlider.value -= Time.deltaTime / fuelFallRate;
        }
        if (fuelSlider.value <= 0)
        {                
            fuelSlider.value = 0;
			playerDead = true;
			//need to put respawn stuff here
			//but for the moment I'm just setting the fuel slider back to full
			//sorry lewis, dont hurt me. :P
			fuelSlider.value = maxFuel;
			                          
        }
        else if (fuelSlider.value >= maxFuel)
        {
            fuelSlider.value = maxFuel;
        }	
    }


    public void BurnObject()
    {
        if (fuelSlider.value >= 0 && canBurnObj == true)
        {

            burnObj.transform.GetChild(0).gameObject.SetActive(true);
            fuelDecrease = 3;
            RemoveFuel();

            if (burnObj.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().isEmitting == false)
            {

            }
            burnObj.SetActive(false);
            canBurnObj = false;

        }


        if (fuelSlider.value <= 0)
        {
            Debug.Log("Not enough Light to push objects.");
            return;
        }

    }

    public void LightObject()
    {
        if (fuelSlider.value >= 0 && canLightObj == true)
        {
            lightObj.transform.GetChild(0).gameObject.SetActive(true);
            fuelDecrease = 5;
            RemoveFuel();
            canLightObj = false;
        }


        if (fuelSlider.value <= 0)
        {
            Debug.Log("Not enough Light to push objects.");
            return;
        }
    }




    public void RemoveFuel()
    {
        fuelSlider.value = fuelSlider.value - fuelDecrease;
        fuelDecrease = 0;
    }

    public void PlayerDeath()
    {


        //player_AnimatorController.Instance.PlayerDie();
        Debug.Log("player dead");


        playerDead = false;
        transform.position = CheckPoint.GetActiveCheckPointPosition();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Oil")
        {
            other.transform.GetChild(0).gameObject.SetActive(false);
            other.transform.GetChild(1).gameObject.SetActive(true);
            fuelSlider.value = maxFuel;
			Debug.Log ("Gained Fuel");
			//other.gameObject.SetActive (false);
        }


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

        if (other.gameObject.tag == "Collectable")
        {
            if (other.name == "red")
            {
                Debug.Log("red");
                gateManager.redCollected += 1;
                other.gameObject.SetActive(false);
            }
            if (other.name == "blue")
            {
                gateManager.blueCollected += 1;
                other.gameObject.SetActive(false);
            }
            if (other.name == "green")
            {
                gateManager.greenCollected += 1;
                other.gameObject.SetActive(false);
            }
        }


        if (other.gameObject.tag == "Finish")
        {
            Debug.Log("finish");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
