using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oil_Rotate : MonoBehaviour {
    public int RotateSpeed;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RotateSpeed = 2;
        transform.Rotate(0, RotateSpeed, 0, Space.World);
    }
}
