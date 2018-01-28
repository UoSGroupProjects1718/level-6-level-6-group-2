using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    public Transform canvas;
    public Transform inventory;
    public Transform pauseM;

    public static bool IsPaused = false;

    private void Awake()
    {
        inventory = canvas.Find("Inventory");
        pauseM = canvas.Find("PauseMenu");
    }

	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    public void Resume ()
    {
        pauseM.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        IsPaused = false;
    }

    void Pause ()
    {
        pauseM.gameObject.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Main Menu");      
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Settings()
    {

    }

    public void ToggleInventory()
    {
        inventory.gameObject.SetActive(!inventory.gameObject.activeInHierarchy);
    }
}
