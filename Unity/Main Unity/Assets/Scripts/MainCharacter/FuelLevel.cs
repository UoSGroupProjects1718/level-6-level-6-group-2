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

    public GameObject PressE;

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
    AudioSource audios;
    public AudioClip pickupAudio;
    public AudioClip burnAudio;
    public AudioClip lightAudio;
    public AudioClip failLightAudio;
    

    // Use this for initialization
    void Start () {
        lantern = lantern.GetComponent<Light>();
        audios = GetComponent<AudioSource>();
         gateManager.GetComponent<GateManager>();
        PressE.SetActive(false);
        lantern.intensity = 8;
        fuelSlider.value = 8;
        maxFuel = 10;
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
           // fuelSlider.value -= Time.time / fuelFallRate;
        }
        if (fuelSlider.value <= 0)
        {                
            fuelSlider.value = 0;
			playerDead = true;
            PlayerDeath();
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
        PressE.SetActive(false);
        if (fuelSlider.value >= 0 && canBurnObj == true)
        {

            burnObj.transform.GetChild(0).gameObject.SetActive(true);
            fuelDecrease = 1;
            RemoveFuel();
            canBurnObj = false;
            burnObj.SetActive(false);
            audios.clip = burnAudio;
            audios.Play();
        }


        if (fuelSlider.value <= 0)
        {
            Debug.Log("Not enough Light to burn objects.");
            audios.clip = failLightAudio;
            audios.Play();
            return;
        }
        

    }

    public void LightObject()
    {
        PressE.SetActive(false);
        if (fuelSlider.value >= 0 && canLightObj == true)
        {
            lightObj.transform.GetChild(0).gameObject.SetActive(true);
            fuelDecrease = 5;
            RemoveFuel();
            audios.clip = lightAudio;
            audios.Play();
            audios.clip = null;
            canLightObj = false;
        }


        if (fuelSlider.value <= 0)
        {
            Debug.Log("Not enough Light to light objects.");
            audios.clip = failLightAudio;
            audios.Play();

            audios.clip = null;
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
            PressE.SetActive(true);
            burnObj = other.gameObject;

            canBurnObj = true;
        }

        if (other.gameObject.tag == "playerLight")
        {
            PressE.SetActive(true);
            canLightObj = true;
            lightObj = other.gameObject;
        }

        if (other.gameObject.tag == "Collectable")
        {
            audios.clip = pickupAudio;
            audios.Play();
            audios.clip = null;
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
            PressE.SetActive(false);
            burnObj = null;
            canBurnObj = false;

        }

        if (other.gameObject.tag == "playerLight")
        {
            PressE.SetActive(false);
            lightObj = null;
            canLightObj = false;
        }

    }

}
