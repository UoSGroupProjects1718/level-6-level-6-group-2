using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableDoors : MonoBehaviour {

    public GateManager gateMan;

    public int requiredFeathers;

    public bool isOpen;

    public bool isTrigger;

	// Use this for initialization
	void Start ()
    {
        isOpen = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gateMan.featherNumberCount >= requiredFeathers && isTrigger && Input.GetKeyDown(KeyCode.Q))
        {
            isOpen = true;
            gateMan.featherNumberCount -= requiredFeathers;
            gateMan.UpdateScore();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isTrigger = false;
        }
    }

    private void OnGUI()
    {
        if (isTrigger && !isOpen && gateMan.featherNumberCount < requiredFeathers)
        {

            GUI.Box(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 200, 50),  requiredFeathers + " Feather Needed");

        }
        if(isTrigger && gateMan.featherNumberCount >= requiredFeathers)
        {

            GUI.Box(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 50, 300, 50), "Press Q To Open Door (Feathers Will Be Used)");

        }
    }
}
