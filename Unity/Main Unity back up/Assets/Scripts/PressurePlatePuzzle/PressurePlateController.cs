using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateController : MonoBehaviour {

    //Pressure plate gameobjects
    public List<GameObject> pressurePlateList = new List<GameObject>();
    public GameObject correctPressure;
    public GameObject bridge;

    public bool hitPlatform;
    public bool puzzleComplete;

    public GameObject fuelLvl;

	// Use this for initialization
	void Start () {
        fuelLvl = GameObject.FindGameObjectWithTag("Player");
        hitPlatform = false;
        puzzleComplete = false;
        //randomly chooseing a pressure plate
        correctPressure = pressurePlateList[Random.Range(0, pressurePlateList.Count)];
        correctPressure.GetComponent<PressurePlate>().correctPlate = true;
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}




    //if that one is triggered
    public void CorrectPlate()
    {
        //activate bridge object
       // bridge.SetActive(true);
        puzzleComplete = true;
        Debug.Log("Correct Pressure Plate");
        
    }

    public void WrongPlate()
    {
        fuelLvl.GetComponent<FuelLevel>().playerDead = true;
    }
    //if not player die
}
