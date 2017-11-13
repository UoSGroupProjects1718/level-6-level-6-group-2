using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour {

    public Objects objectManager;

    void Start()
    {
        objectManager = GetComponent<Objects>();   
    }

    public void hitTarget(bool didHit)
    {
        objectManager.hitTarget(didHit);
    }

}
