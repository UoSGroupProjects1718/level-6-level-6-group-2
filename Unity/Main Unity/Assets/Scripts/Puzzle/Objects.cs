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
 
    public void hitTarget(bool didHit)
    {
        if (didHit)
        {
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
