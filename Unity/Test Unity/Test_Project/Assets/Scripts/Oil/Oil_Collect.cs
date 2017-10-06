using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oil_Collect : MonoBehaviour {
    public GameObject BlueOil;
    public GameObject RedOil;
    public GameObject GreenOil;
    public Light Lantern;

	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Blue-Oil")
        {
            Lantern.color = Color.blue;
            BlueOil.SetActive(false);
        }

        else if (other.gameObject.name == "Red-Oil")
        {
            Lantern.color = Color.red;
            RedOil.SetActive(false);
        }

        else if (other.gameObject.name == "Green-Oil")
        {
            Lantern.color = Color.green;
            GreenOil.SetActive(false);
        }

    }
}
