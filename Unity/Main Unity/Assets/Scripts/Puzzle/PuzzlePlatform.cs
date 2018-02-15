using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

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
            other.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.MouseLook>().SetCursorLock(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Stop");
            other.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.MouseLook>().SetCursorLock(true);
            lInstance.Stopl();
        }
    }
}
