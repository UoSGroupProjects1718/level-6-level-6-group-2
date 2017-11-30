using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelLevel : MonoBehaviour {

    public player_Controller Instance;
    public static CheckPoint checkpoint;

    public Slider fuelSlider;
    public int maxFuel;
    public int fuelFallRate;
    public Light Lantern;

	public GameObject lanterns;
	public GameObject playerHand;
	public GameObject lanternTarget;

	// Use this for initialization
	void Start () {
        fuelSlider.maxValue = maxFuel;
        fuelSlider.value = maxFuel;
        Lantern.GetComponent<Light>();
        Lantern.intensity = 3f;
	}

	// Update is called once per frame
	void Update () {

		lanterns.transform.parent = playerHand.transform;
		lanterns.transform.LookAt (lanternTarget.transform);

        if (fuelSlider.value >= 0)
        {
            fuelSlider.value -= Time.deltaTime / fuelFallRate;
        }
        if (fuelSlider.value <= 0)
        {                
            fuelSlider.value = 0;
			player_Controller.Instance.playerDead = true;
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Oil")
        {
            fuelSlider.value = maxFuel;
			Debug.Log ("Gained Fuel");
			other.gameObject.SetActive (false);
        }
    }
}
