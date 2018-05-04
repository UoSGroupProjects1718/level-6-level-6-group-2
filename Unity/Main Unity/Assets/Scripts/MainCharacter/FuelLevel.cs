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

    //dissapear shader
    public Renderer rend;
    private IEnumerator coroutine;



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
                //dissolving the object
                coroutine = BurnStuff();
                StartCoroutine(coroutine);

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


            Debug.Log("burning");
            burnObj.transform.GetChild(0).gameObject.SetActive(true);
            fuelDecrease = 1;
            RemoveFuel();
            canBurnObj = false;
            Destroy(burnObj);
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

    private IEnumerator BurnStuff()
    {
        rend = burnObj.GetComponent<Renderer>();
       
        rend.material.SetFloat("_Progress", 0.9f);
        yield return new WaitForSeconds(0.1f);
        rend.material.SetFloat("_Progress", 0.8f);
        yield return new WaitForSeconds(0.1f);
        rend.material.SetFloat("_Progress", 0.7f);
        yield return new WaitForSeconds(0.1f);
        rend.material.SetFloat("_Progress", 0.6f);
        yield return new WaitForSeconds(0.1f);
        rend.material.SetFloat("_Progress", 0.5f);
        yield return new WaitForSeconds(0.1f);
        rend.material.SetFloat("_Progress", 0.4f);
        yield return new WaitForSeconds(0.1f);
        rend.material.SetFloat("_Progress", 0.3f);
        yield return new WaitForSeconds(0.1f);
        rend.material.SetFloat("_Progress", 0.2f);
        yield return new WaitForSeconds(0.1f);
        rend.material.SetFloat("_Progress", 0.1f);


        BurnObject();
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



        if (other.gameObject.tag == "burnable")
        {
            PressE.SetActive(true);
            if (gateManager.featherNumberCount > 0)
            {

                burnObj = other.gameObject;
                gateManager.featherNumberCount -= 1;
                gateManager.CheckFeatherCount();
                canBurnObj = true;

            }

        }

        if (other.gameObject.tag == "playerLight")
        {
            PressE.SetActive(true);
            if (gateManager.featherNumberCount > 0)
            {

                canLightObj = true;
                gateManager.featherNumberCount -= 1;
                gateManager.CheckFeatherCount();

                lightObj = other.gameObject;
            }

        }

        if (other.gameObject.tag == "Collectable")
        {
            audios.clip = pickupAudio;
            audios.Play();
            audios.clip = null;                   
            Debug.Log("feather Collected");
            gateManager.featherNumberCount += 1;
            gateManager.CheckFeatherCount();
            other.gameObject.SetActive(false);
            
            /*if (other.name == "blue")
            {
                gateManager.blueCollected += 1;
                other.gameObject.SetActive(false);
            }
            if (other.name == "green")
            {
                gateManager.greenCollected += 1;
                other.gameObject.SetActive(false);
            }
            */
        }
        if (other.gameObject.tag == "Hazard")
        {
            if (gateManager.featherNumberCount > 0)
            {            
                gateManager.featherNumberCount -= 1;
                gateManager.CheckFeatherCount();
                //pain sound         
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
