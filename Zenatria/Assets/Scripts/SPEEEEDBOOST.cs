using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPEEEEDBOOST : MonoBehaviour
{

    private float timescale = 1.0f;
    public GameObject pauseMenu;

    public void ChangeTimescale()
    {
        if (timescale == 1.0f)
        {
            timescale = 2.0f;

        }
        else if (timescale == 2.0f || timescale == 0.0f)
        {
            timescale = 1.0f;
        }



        Time.timeScale = timescale;

    }


    public void PauseMenu()
    {
        timescale = 0.0f;
        Time.timeScale = timescale;
        pauseMenu.SetActive(true);
    }

    public void ResumeTime()
    {
        timescale = 1.0f;
        Time.timeScale = timescale;
        pauseMenu.SetActive(false);
    }



}
