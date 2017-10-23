using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelLevel : MonoBehaviour {

    public Slider fuelSlider;
    public int maxFuel;
    public int fuelFallRate;

	// Use this for initialization
	void Start () {

        fuelSlider.maxValue = maxFuel;
        fuelSlider.value = maxFuel;
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(fuelSlider.value >=0)
        {
            fuelSlider.value -= Time.deltaTime / fuelFallRate;
        }
        else if(fuelSlider.value <=0)
        {
            fuelSlider.value = 0;
        }
        else if (fuelSlider.value >= maxFuel)
        {
            fuelSlider.value = maxFuel;
        }

	}
}
