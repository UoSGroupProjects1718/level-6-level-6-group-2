using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour {
    public GameObject exitNode;
    public Material exitActivated;
    public Material exitDeactivated;
    public bool exitReached = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void hitTarget(bool didHit)
    {
        if (didHit)
        {
            exitNode.GetComponent<Renderer>().material = exitActivated;
            exitReached = true;
        }
        else
        {
            exitNode.GetComponent<Renderer>().material = exitDeactivated;
            exitReached = false;
        }
    }

}
