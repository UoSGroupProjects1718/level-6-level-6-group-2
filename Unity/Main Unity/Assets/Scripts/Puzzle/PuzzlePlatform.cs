using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePlatform : MonoBehaviour {

    public laser lInstance;

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
