using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePlatform : MonoBehaviour {

    //flickering light
    public float timeOn = 0.1f;
    public float timeOff = 0.5f;
    private float changeTime = 0;
    Light lights;

    public laser lInstance;

    private void Start()
    {
        lights = gameObject.transform.GetChild(0).GetComponent<Light>();
    }
    private void Update()
    {
        if (Time.time > changeTime)
        {
            lights.enabled = !lights.enabled;
            if (lights.enabled)
            {
                changeTime = Time.time + timeOn;
            }
            else
            {
                changeTime = Time.time + timeOff;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Hit");
            lInstance.StartL();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Stop");
            lInstance.Stopl();
        }
    }
}
