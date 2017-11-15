using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour {

    public Objects objectManager;
    public GameObject destroyed;

    void Start()
    {
        objectManager = GetComponent<Objects>();
    }

    public void hitTarget(bool didHit)
    {
        if (didHit)
        {
            Instantiate(destroyed, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}

