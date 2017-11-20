using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelLevel : MonoBehaviour {

    public Slider fuelSlider;
    public int maxFuel;
    public int fuelFallRate;
    public Light Lantern;

	public player_Controller playerController; // reference to player controller
	public bool playerDead;

	public GameObject lanterns;
	public GameObject playerHAnd;
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

		lanterns.transform.parent = playerHAnd.transform;
		lanterns.transform.LookAt (lanternTarget.transform);

        if (fuelSlider.value >= 0)
        {
            fuelSlider.value -= Time.deltaTime / fuelFallRate;
			Lantern.intensity = fuelSlider.value;
        }
        else if (fuelSlider.value <= 0)
        {
            fuelSlider.value = 0;
            playerDead = true;
        }
        else if (fuelSlider.value >= maxFuel)
        {
            fuelSlider.value = maxFuel;
        }

		if (playerDead) {
			playerController.PlayerDeath();
			Lantern.intensity = 0;
		}
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Oil")
        {
            fuelSlider.value = maxFuel;
        }
    }
}
