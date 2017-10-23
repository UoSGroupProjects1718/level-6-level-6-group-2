using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (LineRenderer))]

public class Lazer : MonoBehaviour {

    private LineRenderer lr;

	// Use this for initialization
	void Start () {
        lr = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if(hit.collider.tag == "Mirror")
            {
                lr.SetPosition(1, new Vector3(0, 0, hit.distance));
                

            }
        }
        else
        {
            lr.SetPosition(1, new Vector3(0, 0, 5000));
        }
		
	}
}
