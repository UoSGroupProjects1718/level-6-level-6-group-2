using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    public bool canMove;
    public float moveSpeed;
    public Transform a, b;
    


	// Use this for initialization
	void Start () {
        canMove = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (canMove)
        {
            float pingPong = Mathf.PingPong(Time.time * moveSpeed, 1);
            transform.position = Vector3.Lerp(a.position, b.position, pingPong);
            
        }
	}

    

}
