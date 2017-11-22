using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour {
    public GameObject exitNode;
    public Material exitActivated;
    public Material exitDeactivated;
    public bool exitReached = false;
    public GameObject destroyed;
    public Transform blockade;

	//to turn the lights on when completed puzzle
	public GameObject light1;
	public GameObject light2;
 
    public void hitTarget(bool didHit)
    {
        if (didHit)
        {
			light1.SetActive (true);
			light2.SetActive (true);

            exitNode.GetComponent<Renderer>().material = exitActivated;
            exitReached = true;
            Instantiate(destroyed, blockade.position, blockade.rotation);
            Destroy(exitNode);
        }
        else
        {
            exitNode.GetComponent<Renderer>().material = exitDeactivated;
            exitReached = false;
        }
    }
}
