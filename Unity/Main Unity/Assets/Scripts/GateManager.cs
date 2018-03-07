using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GateManager : MonoBehaviour {

   // public GameObject[] redGates;
   // public GameObject[] blueGates;
  //  public GameObject[] greenGates;

    //public bool redOpen;
    //public bool blueOpen;
    //public bool greenOpen;

    //public int redCollected;
    //public int blueCollected;
    //public int greenCollected;

    public Text scoreText;
    public int featherNumberCount;

    // Use this for initialization
    void Start() {

        featherNumberCount = 0;
        UpdateScore();

    }

    // Update is called once per frame
    void Update() {
        /*
        if (redCollected >= 3)
            redOpen = true;


        if (blueCollected >= 3)
            blueOpen = true;


        if (greenCollected >= 3)
            greenOpen = true;


        if (redOpen)
        {
            for (int i = 0; i < redGates.Length; i++)
            {
                redGates[i].SetActive(false);
            }
        }

        if (blueOpen)
        {
            for (int i = 0; i < blueGates.Length; i++)
            {
                blueGates[i].SetActive(false);
            }
        }

        if (greenOpen)
        {
            for (int i = 0; i < greenGates.Length; i++)
            {
                greenGates[i].SetActive(false);
            }
        }
        */
    }


     public void UpdateScore()
    {

        scoreText.text = featherNumberCount.ToString();

    }
}
