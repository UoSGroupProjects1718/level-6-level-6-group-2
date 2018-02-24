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

	public Camera thisOverHeadCamera;

	public int cameraNum;
    public GameObject playercontroller;

    public GameObject lmbObject;

    private void Start()
    {
        lmbObject.SetActive(false);
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
            //other.gameObject.GetComponent<FirstPersonController>().overHeadCamera = thisOverHeadCamera;
            //other.gameObject.GetComponent<FirstPersonController>().ShowOverHeadCamera();
            lmbObject.SetActive(true);
            other.gameObject.GetComponent<FirstPersonController>().SetCursorLock(false);
            //other.gameObject.GetComponent<MouseLook>().m_cursorIsLocked = false;
            // Cursor.visible = true;
            // Cursor.lockState = CursorLockMode.Confined;

            //other.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.MouseLook>().SetCursorLock(false);
            lInstance.StartL();

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Stop");
            lmbObject.SetActive(false);
            other.gameObject.GetComponent<FirstPersonController>().SetCursorLock(true);
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
            //other.gameObject.GetComponent<FirstPersonController>().overHeadCamera = null;
            //other.gameObject.GetComponent<FirstPersonController>().ShowFirstPersonCamera();
            //mouseLook.m_cursorIsLocked = true;
            // other.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.MouseLook>().SetCursorLock(true);
            lInstance.Stopl();
        }
    }
}
