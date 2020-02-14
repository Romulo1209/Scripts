using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    bool onPause;

    public GameObject pauseMenu;
    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(onPause == true) { onPause = false; } else { onPause = true; }
        }

        if(onPause == true)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        onPause = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
